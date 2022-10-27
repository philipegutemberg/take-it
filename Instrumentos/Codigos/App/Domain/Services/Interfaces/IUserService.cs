using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Services.Interfaces
{
    public interface IUserService<TUser>
        where TUser : User
    {
        Task<TUser> Create(TUser user);

        Task<TUser> Get(string username);
    }
}