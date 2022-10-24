using System.Threading.Tasks;
using Domain.Models;
using Domain.Services.Interfaces;
using Ethereum.Nethereum.SmartContracts.ERC721Mintable.Deployment;

namespace Ethereum.Nethereum
{
    internal class ERC271MintableCreationService : ITokenCreationService
    {
        private readonly DeploymentService _eventTokenDeploymentService;

        public ERC271MintableCreationService(DeploymentService eventTokenDeploymentService)
        {
            _eventTokenDeploymentService = eventTokenDeploymentService;
        }

        public async Task<string> Create(Event @event)
        {
            return await _eventTokenDeploymentService.Deploy(@event.Title, @event.Ticker);
        }
    }
}