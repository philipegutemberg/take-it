using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Create(User user);

        Task<User> Get(string username);
    }
}