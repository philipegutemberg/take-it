using System.Threading.Tasks;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class UserService<TUser> : IUserService<TUser>
        where TUser : User
    {
        private readonly IUserRepository<TUser> _userRepository;

        public UserService(IUserRepository<TUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<TUser> Create(TUser user)
        {
            return await _userRepository.Insert(user);
        }

        public async Task<TUser> Get(string username)
        {
            return await _userRepository.GetByUsername(username);
        }
    }
}