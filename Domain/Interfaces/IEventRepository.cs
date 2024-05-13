using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEventRepository
    {
        Task AddEvent(Event eventData);

        Task DeleteEvent(Guid eventId);

        Task UpdateEvent(Event eventData);

        Task<Event> GetEvent(Guid eventId);

        Task<List<Event>> GetEvents();
    }
}
