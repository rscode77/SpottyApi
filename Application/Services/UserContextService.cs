using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
	public class UserContextService : IUserContextService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserContextService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public ClaimsPrincipal User => _httpContextAccessor.HttpContext!.User;

		public string? GetUserId => User is null ? null : User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

		public string? GetUserRole => User is null ? null : User.FindFirst(ClaimTypes.Role)!.Value;

		public string? GetUserConfirmation => User is null ? null : User.FindFirst(ClaimTypes.Authentication)!.Value;
	}
}