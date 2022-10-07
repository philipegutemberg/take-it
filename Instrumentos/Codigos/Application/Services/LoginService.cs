using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;

namespace Application.Services
{
    public class LoginService
    {
        private readonly UserService _userService;
        private readonly IUserRepository _userRepository;

        public LoginService(UserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }
        
        public async Task<User> Login(string username, string password)
        {
            User user = await _userRepository.GetUser(username);
            if (user == null)
                throw new UserNotFoundException(username);

            await _userService.SignIn(user);

            return user;
        }
    }
}