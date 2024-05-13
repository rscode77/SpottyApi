using Domain.Entities;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using SpottyApi;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly AuthenticationSettings _authenticationSettings;

        public AuthService(
            IAuthRepository authRepository,
            AuthenticationSettings authenticationSettings
        )
        {
            _authRepository = authRepository;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<string> Login(User userData)
        {
            User user = await _authRepository.Login(userData);

            return GenerateJwtToken(user);
        }

        public async Task<User> Register(User userData)
        {
            return await _authRepository.Register(userData);
        }

        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey)
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_authenticationSettings.JwtExpireDays);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,  $"{user.Id}"),
                new Claim(ClaimTypes.Name, $"{user.Username}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim(ClaimTypes.Authentication, user.UserConfirmed ? "1" : "0")
            };

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task ActivateUserAccount(User userData)
        {
            await _authRepository.ActivateUserAccount(userData);
        }
    }
}