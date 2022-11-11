using System.Net.Mime;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;

namespace Ethereum.Nethereum.Services
{
    internal class DeploymentService
    {
        private readonly OwnerAccountsService _ownerAccountsService;
        private readonly Web3Service _web3Service;

        public DeploymentService(OwnerAccountsService ownerAccountsService, Web3Service web3Service)
        {
            _ownerAccountsService = ownerAccountsService;
            _web3Service = web3Service;
        }

        public async Task<string> Deploy<TContract>(TContract deploymentMessage)
            where TContract : ContractDeploymentMessage, new()
        {
            var web3 = _web3Service.GetWeb3(_ownerAccountsService.GetContractOwner());

            var deploymentHandler = web3.Eth.GetContractDeploymentHandler<TContract>();

            /* Aumentando o valor do gas para a transação acontecer mais rápido. */
            var estimate = await deploymentHandler.EstimateGasAsync(deploymentMessage);
            deploymentMessage.Gas = (int)((long)estimate.Value * 1.3m);

            TransactionReceipt? transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
            return transactionReceipt.ContractAddress;
        }
    }
}