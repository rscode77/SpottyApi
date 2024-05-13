using Application.ApplicationUser;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpottyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserActivityController : ControllerBase
    {
        private readonly IUserActivityService _userActivityService;
        private readonly IMapper _mapper;

        public UserActivityController(IUserActivityService userActivityService, IMapper mapper)
        {
            _userActivityService = userActivityService;
            _mapper = mapper;
        }

        [HttpPost("GetUsersActivity")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersActivity()
        {
            var activities = await _userActivityService.GetUsersActivity();

            var userActivityDtos = activities.AsQueryable().ProjectTo<UserActivityDto>(_mapper.ConfigurationProvider).ToList();

            return Ok(new ServiceResponse<List<UserActivityDto>> { Data = userActivityDtos });
        }

        [HttpPut("UpdateUserLocation")]
        public async Task<IActionResult> UpdateUserLocation([FromBody] UpdateUserLocationDto locationData)
        {
            await _userActivityService.UpdateUserLocation(locationData.Lat, locationData.Lon);

            return Ok(new ServiceResponse<string> { Message = $"User location updated." });
        }
    }
}
