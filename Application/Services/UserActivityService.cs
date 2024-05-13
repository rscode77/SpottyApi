using Application.ApplicationUser;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository _userActivityRepository;
        IHubContext<UserActivityHub> _hubContext;

        public UserActivityService(IUserActivityRepository userActivityRepository, IHubContext<UserActivityHub> hubContext)
        {
            _userActivityRepository = userActivityRepository;
            _hubContext = hubContext;
        }

        public async Task<List<UserActivity>> GetUsersActivity()
        {
            return await _userActivityRepository.GetUsersActivity();
        }

        public async Task UpdateUserLocation(double lat, double lon)
        {
            await _userActivityRepository.UpdateUserLocation(lat, lon);
        }

        public async Task NotifyUsersAboutActivity(UserActivityDto userActivity)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveUserActivity", userActivity);
        }
    }
}
