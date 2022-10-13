using System.Numerics;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using Ethereum.Nethereum.TicketTokenSmartContract.Deployment;

namespace Ethereum.Nethereum
{
    internal class TicketTokenCreationService : ITokenCreationService
    {
        private readonly TicketTokenDeploymentService _eventTokenDeploymentService;

        public TicketTokenCreationService(TicketTokenDeploymentService eventTokenDeploymentService)
        {
            _eventTokenDeploymentService = eventTokenDeploymentService;
        }
        
        public async Task<string> Create(string name, string symbol)
        {
            return await _eventTokenDeploymentService.Deploy(name, symbol);
        }
    }
}