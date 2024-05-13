using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SpottyDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public AuthRepository(SpottyDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task ActivateUserAccount(User userData)
        {
            User? user = null;

            if (userData.Email != null)
                user = _dbContext.Users.FirstOrDefault(x => x.Email == userData.Email);
            else if (userData.Username != null)
                user = _dbContext.Users.FirstOrDefault(x => x.Username == userData.Username);

            if (user == null)
                throw new NotFoundException("User not found.");

            if (user.UserConfirmed == true)
                throw new BadRequestException("User alerdy confirmed.");

            user.UserConfirmed = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> Login(User userData)
        {
            User? user = null;

            if (userData.Email != null)
                user = await _dbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == userData.Email);
            else if (userData.Username != null)
                user = await _dbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Username == userData.Username);

            if (user == null)
                throw new Exception("Invalid username or password.");

            bool isPasswrodValid = _passwordHasher.VerifyPassword(user.Password, userData.Password);

            if (!isPasswrodValid)
                throw new Exception("Invalid username or password.");

            return user;
        }

        public async Task<User> Register(User userData)
        {
            userData.Password = _passwordHasher.HashPassword(userData.Password);

            await _dbContext.Users.AddAsync(userData);

            await _dbContext.SaveChangesAsync();

            return userData;
        }
    }
}