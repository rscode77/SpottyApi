using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserActivityService
    {
        Task<List<UserActivity>> GetUsersActivity();
        Task UpdateUserLocation(double lat, double lon);
    }
}
