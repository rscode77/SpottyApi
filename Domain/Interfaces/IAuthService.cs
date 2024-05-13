using Domain.Entities;

namespace Domain.Interfaces
{
	public interface IAuthService
	{
		Task ActivateUserAccount(User userData);

		Task<string> Login(User userData);

		Task<User> Register(User userData);
	}
}