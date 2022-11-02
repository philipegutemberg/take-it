using System.Threading.Tasks;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class SpecialUserService : ISpecialUserService
    {
        private readonly IBackofficeUserRepository _backofficeUserRepository;
        private readonly IGatekeeperUserRepository _gatekeeperUserRepository;

        public SpecialUserService(IBackofficeUserRepository backofficeUserRepository, IGatekeeperUserRepository gatekeeperUserRepository)
        {
            _backofficeUserRepository = backofficeUserRepository;
            _gatekeeperUserRepository = gatekeeperUserRepository;
        }

        public async Task<BackOfficeUser> CreateBackOfficeUser(BackOfficeUser user)
        {
            return await _backofficeUserRepository.Insert(user);
        }

        public async Task<GatekeeperUser> CreateGatekeeperUser(GatekeeperUser user)
        {
            return await _gatekeeperUserRepository.Insert(user);
        }
    }
}