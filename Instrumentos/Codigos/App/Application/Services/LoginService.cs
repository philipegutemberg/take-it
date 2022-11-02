using System.Threading.Tasks;
using Domain.Models.Users;
using Domain.Services.Interfaces;

namespace Application.Services
{
    public class LoginService
    {
        private readonly IGenericUserService<GenericUser> _userService;
        private readonly HashService _hashService;

        public LoginService(IGenericUserService<GenericUser> userService, HashService hashService)
        {
            _userService = userService;
            _hashService = hashService;
        }

        public async Task<GenericUser?> TryLogin(string username, string password)
        {
            password = _hashService.GetHash(password);
            GenericUser user = await _userService.Get(username);

            if (password == user.Password)
                return user;
            return null;
        }
    }
}