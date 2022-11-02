using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Services.Interfaces
{
    public interface IGenericUserService<TUser>
        where TUser : GenericUser
    {
        Task<TUser> Get(string username);
    }
}