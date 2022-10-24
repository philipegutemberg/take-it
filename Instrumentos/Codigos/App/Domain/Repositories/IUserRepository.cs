using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> Save(User user);

        Task<User> GetUser(string username);

        Task<Customer> GetCustomer(string username);
    }
}