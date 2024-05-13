using Domain.Entities;

namespace Domain.Interfaces
{
	public interface IAuthRepository
	{
		Task<User> Login(User userData);

		Task<User> Register(User userData);

		Task ActivateUserAccount(User userDaat);
	}
}