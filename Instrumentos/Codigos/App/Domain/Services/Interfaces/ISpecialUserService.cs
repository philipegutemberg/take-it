using System.Threading.Tasks;
using Domain.Models.Users;

namespace Domain.Services.Interfaces
{
    public interface ISpecialUserService
    {
        Task<BackOfficeUser> CreateBackOfficeUser(BackOfficeUser user);

        Task<GatekeeperUser> CreateGatekeeperUser(GatekeeperUser user);
    }
}