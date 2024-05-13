using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserAcitivityRepository : IUserActivityRepository
    {
        private readonly SpottyDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IHubContext<UserActivityHub> _hubContext;

        public UserAcitivityRepository(SpottyDbContext dbContext, IUserContextService userContextService, IHubContext<UserActivityHub> hubContext)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _hubContext = hubContext;
        }

        public async Task<List<UserActivity>> GetUsersActivity()
        {
            if (_dbContext.UserActivity.ToList().Count == 0)
                return new List<UserActivity>();

            return await _dbContext.UserActivity.Include(x => x.User).Where(x => x.User!.IsOnline == true).ToListAsync();
        }

        public async Task NotifyUsersAboutActivity(UserActivity userActivity)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveUserActivity", userActivity);
        }

        public async Task UpdateUserLocation(double lat, double lon)
        {
            if (_userContextService.GetUserId == null)
                throw new Exception("User not found");

            var user = _dbContext.UserActivity.FirstOrDefault(x => x.User!.Id == Guid.Parse(_userContextService.GetUserId));

            if (user == null)
                throw new Exception("User not found");

            user.Lat = lat;
            user.Lon = lon;

            await _dbContext.SaveChangesAsync();

            await NotifyUsersAboutActivity(user);
        }
    }
}
