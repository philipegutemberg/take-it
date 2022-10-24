using System.Threading.Tasks;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Create(User user)
        {
            return await _userRepository.Save(user);
        }

        public async Task<User> Get(string username)
        {
            return await _userRepository.GetUser(username);
        }
    }
}