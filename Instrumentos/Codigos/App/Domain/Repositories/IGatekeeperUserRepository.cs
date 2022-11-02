using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Repositories
{
    public interface IGatekeeperUserRepository
    {
        Task<GatekeeperUser> Insert(GatekeeperUser user);
    }
}