using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task AddEvent(Event eventData)
        {
            await _eventRepository.AddEvent(eventData);
        }

        public async Task DeleteEvent(Guid eventId)
        {
            await _eventRepository.DeleteEvent(eventId);
        }

        public async Task<Event> GetEvent(Guid eventId)
        {
            return await _eventRepository.GetEvent(eventId);
        }

        public async Task<List<Event>> GetEvents()
        {
            return await _eventRepository.GetEvents();
        }

        public async Task UpdateEvent(Event eventData)
        {
            await _eventRepository.UpdateEvent(eventData);
        }
    }
}
