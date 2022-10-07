using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Users;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUser(string username);
    }
}