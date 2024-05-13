using Application.ApplicationUser;
using Application.Interfaces;
using ApplicationUser.User;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace SpottyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;

        public EventController(IMapper mapper, IEventService eventService)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpPost("GetEvent")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEvent([FromBody] Guid eventId)
        {
            var result = await _eventService.GetEvent(eventId);

            var eventDto = _mapper.Map<EventDto>(result);

            eventDto.EventParticipants = result.EventParticipant
             .Select(participant => _mapper.Map<EventParticipantDto>(participant))
             .ToList();

            eventDto.Creator = _mapper.Map<UserDto>(result.Creator);

            return Ok(new ServiceResponse<EventDto> { Data = eventDto });
        }

        [HttpGet("GetEvents")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventService.GetEvents();

            var eventDtos = events.AsQueryable().ProjectTo<EventDto>(_mapper.ConfigurationProvider).ToList();

            return Ok(new ServiceResponse<List<EventDto>> { Data = eventDtos });
        }

        [HttpPost("AddEvent")]
        public async Task<IActionResult> AddEvent([FromBody] AddEventDto eventData)
        {
            await _eventService.AddEvent(_mapper.Map<Event>(eventData));

            return Ok(new ServiceResponse<string> { Message = "Event added." });
        }
    }
}
