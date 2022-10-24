using System.Threading.Tasks;

namespace Ethereum.Nethereum.SmartContracts.ERC721Mintable.Deployment
{
    internal class DeploymentService
    {
        private readonly Services.DeploymentService _deploymentService;

        public DeploymentService(Services.DeploymentService deploymentService)
        {
            _deploymentService = deploymentService;
        }

        public async Task<string> Deploy(string tokenName, string tokenSymbol)
        {
            var deploymentMessage = new DeploymentMessage
            {
                TokenName = tokenName,
                TokenSymbol = tokenSymbol
            };

            return await _deploymentService.Deploy(deploymentMessage);
        }
    }
}