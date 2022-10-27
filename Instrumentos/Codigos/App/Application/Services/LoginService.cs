using System.Threading.Tasks;
using Domain.Models.Users;
using Domain.Services.Interfaces;

namespace Application.Services
{
    public class LoginService
    {
        private readonly IUserService<User> _userService;
        private readonly HashService _hashService;

        public LoginService(IUserService<User> userService, HashService hashService)
        {
            _userService = userService;
            _hashService = hashService;
        }

        public async Task<User?> TryLogin(string username, string password)
        {
            password = _hashService.GetHash(password);
            User user = await _userService.Get(username);

            if (password == user.Password)
                return user;
            return null;
        }
    }
}