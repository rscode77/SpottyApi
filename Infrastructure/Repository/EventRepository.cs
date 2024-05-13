using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly SpottyDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public EventRepository(SpottyDbContext spottyDbContext, IUserContextService userContextService)
        {
            _dbContext = spottyDbContext;
            _userContextService = userContextService;
        }

        public async Task AddEvent(Event eventData)
        {
            if (_userContextService.GetUserId == null)
                throw new Exception("User not found");

            eventData.CreatorId = Guid.Parse(_userContextService.GetUserId);

            await _dbContext.Events.AddAsync(eventData);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEvent(Guid eventId)
        {
            var @event = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventId);

            if (@event == null)
                throw new Exception("Event not found");

            _dbContext.Events.Remove(@event);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Event> GetEvent(Guid eventId)
        {
            var @event = await _dbContext.Events.Include(u => u.Creator).Include(u => u.EventParticipant).FirstOrDefaultAsync(x => x.Id == eventId);

            if (@event == null)
                throw new Exception("Event not found");

            return @event;
        }

        public async Task<List<Event>> GetEvents()
        {
            return await _dbContext.Events.Include(u => u.Creator).Include(u => u.EventParticipant).Where(x => x.Date > DateTime.Now).ToListAsync();
        }

        public async Task UpdateEvent(Event eventData)
        {
            var @event = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventData.Id);

            @event = eventData;

            await _dbContext.SaveChangesAsync();
        }
    }
}