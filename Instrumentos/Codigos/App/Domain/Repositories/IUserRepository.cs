using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Repositories
{
    public interface IUserRepository<TUser>
        where TUser : User
    {
        Task<TUser> Save(TUser user);

        Task<TUser> GetByUsername(string username);
    }
}