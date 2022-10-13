using System.Numerics;
using System.Threading.Tasks;
using Ethereum.Nethereum.Services;

namespace Ethereum.Nethereum.TicketTokenSmartContract.Deployment
{
    internal class TicketTokenDeploymentService
    {
        private readonly DeploymentService _deploymentService;

        public TicketTokenDeploymentService(DeploymentService deploymentService)
        {
            _deploymentService = deploymentService;
        }

        public async Task<string> Deploy(string tokenName, string tokenSymbol)
        {
            var deploymentMessage = new TicketTokenDeploymentMessage()
            {
                TokenName = tokenName,
                TokenSymbol = tokenSymbol
            };

            return await _deploymentService.Deploy(deploymentMessage);
        }
    }
}