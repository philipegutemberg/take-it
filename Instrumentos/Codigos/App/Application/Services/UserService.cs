using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Users;

namespace Application.Services
{
    public class UserService
    {
        public Task SignIn(User user) => Task.CompletedTask;
    }
}