using Application.ApplicationUser;
using ApplicationUser.User;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Queries;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpottyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("GetUserById")]
        public async Task<ActionResult<UserDto>> GetUserById([FromBody] Guid id)
        {
            var user = await _userService.GetUserById(id);

            return Ok(new ServiceResponse<UserDto> { Data = _mapper.Map<UserDto>(user) });
        }

        [HttpPost("GetUserByEmail")]
        public async Task<ActionResult<UserDto>> GetUserByEmail([FromBody] string email)
        {
            var user = await _userService.GetUserByEmail(email);

            return Ok(new ServiceResponse<UserDto> { Data = _mapper.Map<UserDto>(user) });
        }

        [HttpPost("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers(
            [FromBody] PaginationSearchQuery query
        )
        {
            var users = await _userService.GetAllUsers(query);

            return Ok(
                new ServiceResponse<IEnumerable<UserDto>>
                {
                    Data = users.Items.Select(e => _mapper.Map<UserDto>(e)).AsEnumerable()
                }
            );
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto user)
        {
            await _userService.UpdateUser(_mapper.Map<User>(user));

            return Ok(new ServiceResponse<UserDto> { Message = "User updated." });
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] Guid id)
        {
            await _userService.DeleteUser(id);

            return Ok(new ServiceResponse<Task> { Message = "User deleted." });
        }
    }
}