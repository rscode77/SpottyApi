using Application.ApplicationUser;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpottyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("ActivateUserAccount")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ActivateUserAccount([FromBody] ActivateUserAccountDto activateAccountDto)
        {
            await _authService.ActivateUserAccount(_mapper.Map<User>(activateAccountDto));

            return Ok(new ServiceResponse<string> { Message = $"User activated." });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userData)
        {
            string token = await _authService.Login(_mapper.Map<User>(userData));

            return Ok(new ServiceResponse<string> { Data = token, Message = $"Hello {userData.Username ?? userData.Email}" });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto userData)
        {
            await _authService.Register(_mapper.Map<User>(userData));

            return Ok(new ServiceResponse<Task> { Message = $"User added {userData.Username}." });
        }
    }
}