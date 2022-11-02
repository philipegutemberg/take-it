using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Repositories
{
    public interface IBackofficeUserRepository
    {
        Task<BackOfficeUser> Insert(BackOfficeUser user);
    }
}