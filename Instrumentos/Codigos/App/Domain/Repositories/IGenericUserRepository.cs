using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Repositories
{
    public interface IGenericUserRepository<TUser>
        where TUser : GenericUser
    {
        Task<TUser> GetByUsername(string username);
    }
}