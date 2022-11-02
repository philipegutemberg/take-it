using System.Threading.Tasks;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class GenericUserService : IGenericUserService<GenericUser>
    {
        private readonly IGenericUserRepository<GenericUser> _userRepository;

        public GenericUserService(IGenericUserRepository<GenericUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GenericUser> Get(string username)
        {
            return await _userRepository.GetByUsername(username);
        }
    }
}