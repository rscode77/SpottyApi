using System.Security.Claims;

namespace Application.Interfaces
{
	public interface IUserContextService
	{
		string? GetUserId { get; }
		string? GetUserRole { get; }
		string? GetUserConfirmation { get; }
		ClaimsPrincipal User { get; }
	}
}