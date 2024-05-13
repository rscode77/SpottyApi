using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserActivityRepository
    {
        Task<List<UserActivity>> GetUsersActivity();
        Task UpdateUserLocation(double lat, double lon);
        Task NotifyUsersAboutActivity(UserActivity userActivity);
    }
}
