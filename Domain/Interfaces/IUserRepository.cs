using Domain.Entities;
using Domain.Queries;

namespace Domain.Interfaces
{
	public interface IUserRepository
	{
		Task<User> GetUserById(Guid id);

		Task<User> GetUserByEmail(string email);

		Task<PageResult<User>> GetAllUsers(PaginationSearchQuery query);

		Task UpdateUser(User user);

		Task DeleteUser(Guid id);
	}
}