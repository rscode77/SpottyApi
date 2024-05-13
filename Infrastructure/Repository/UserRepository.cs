using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Queries;
using Infrastructure.Authorization;
using Infrastructure.Exceptions;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SpottyDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        private readonly IPasswordHasher _passwordHasher;

        public UserRepository(
            SpottyDbContext dbContext,
            IAuthorizationService authorizationService,
            IUserContextService userContextService,
            IPasswordHasher passwordHasher
        )
        {
            _dbContext = dbContext;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _passwordHasher = passwordHasher;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            var authorizeService = _authorizationService
                .AuthorizeAsync(
                    _userContextService.User,
                    user,
                    new ResourceOperationRequirement(ResourceOperation.Delete)
                )
                .Result;

            if (!authorizeService.Succeeded)
                throw new ForbiddenException("You are not allowed to delete this user.");

            if (user == null)
                throw new NotFoundException("User not found");

            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<User>> GetAllUsers(PaginationSearchQuery query)
        {
            var baseQuery = _dbContext
                .Users.Where(r =>
                    query == null || r.Username.ToLower().Contains(query.SearchPhrase.ToLower())
                )
                .Include(u => u.UserProfiles)
                .Include(u => u.Role);

            var users = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();

            var result = new PageResult<User>(
                users,
                baseQuery.Count(),
                query.PageSize,
                query.PageNumber
            );

            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User? user = await _dbContext
                .Users.Include(u => u.UserProfiles)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new NotFoundException("User not found");

            return user;
        }

        public async Task<User> GetUserById(Guid id)
        {
            User? user = await _dbContext
                .Users.Include(u => u.UserProfiles)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new NotFoundException("User not found");

            return user;
        }

        public async Task UpdateUser(User userData)
        {
            var userId = _userContextService.GetUserId;
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
                throw new NotFoundException("User not found");

            var authorizeService = await _authorizationService.AuthorizeAsync(
                _userContextService.User,
                user,
                new ResourceOperationRequirement(ResourceOperation.Update)
            );

            if (!authorizeService.Succeeded)
                throw new ForbiddenException("You are not allowed to update this user.");

            if (!string.IsNullOrEmpty(userData.Username))
                user.Username = userData.Username;

            if (!string.IsNullOrEmpty(userData.Email))
                user.Email = userData.Email;

            if (!string.IsNullOrEmpty(userData.Password))
            {
                var newPassword = _passwordHasher.HashPassword(userData.Password);
                if (newPassword != user.Password)
                    user.Password = newPassword;
            }

            if (user.UserProfiles != null && string.IsNullOrEmpty(user.UserProfiles.AvatarUrl))
                user.UserProfiles.AvatarUrl = userData.UserProfiles.AvatarUrl;

            await _dbContext.SaveChangesAsync();
        }
    }
}