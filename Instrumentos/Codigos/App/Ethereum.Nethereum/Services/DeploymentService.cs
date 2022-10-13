using System.Threading.Tasks;
using Ethereum.Nethereum.Services.Interfaces;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;

namespace Ethereum.Nethereum.Services
{
    internal class DeploymentService
    {
        private readonly IMainAccountFaucet _mainAccountFaucet;
        
        public DeploymentService(IMainAccountFaucet mainAccountFaucet)
        {
            _mainAccountFaucet = mainAccountFaucet;
        }

        public async Task<string> Deploy<TContract>(TContract deploymentMessage)
            where TContract : ContractDeploymentMessage, new()
        {
            var deploymentHandler = _mainAccountFaucet.GetWeb3ETH().GetContractDeploymentHandler<TContract>();
            TransactionReceipt? transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
            return transactionReceipt.ContractAddress;
        }
    }
}