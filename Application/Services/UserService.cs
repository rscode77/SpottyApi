using Domain.Entities;
using Domain.Interfaces;
using Domain.Queries;

namespace Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task DeleteUser(Guid id)
		{
			await _userRepository.DeleteUser(id);
		}

		public async Task<PageResult<User>> GetAllUsers(PaginationSearchQuery query)
		{
			return await _userRepository.GetAllUsers(query);
		}

		public async Task<User> GetUserByEmail(string email)
		{
			return await _userRepository.GetUserByEmail(email);
		}

		public async Task<User> GetUserById(Guid id)
		{
			return await _userRepository.GetUserById(id);
		}

		public async Task UpdateUser(User user)
		{
			await _userRepository.UpdateUser(user);
		}
	}
}