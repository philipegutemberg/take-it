using System.Numerics;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using Ethereum.Nethereum.Services;
using Ethereum.Nethereum.Services.Interfaces;
using Ethereum.Nethereum.TicketTokenSmartContract.Functions;

namespace Ethereum.Nethereum
{
    internal class TicketTokenService : ITokenService
    {
        private readonly IMainAccountFaucet _mainAccountFaucet;
        private readonly ITokenHolderAccountFaucet _tokenHolderAccountFaucet;

        public TicketTokenService(IMainAccountFaucet mainAccountFaucet, ITokenHolderAccountFaucet tokenHolderAccountFaucet)
        {
            _mainAccountFaucet = mainAccountFaucet;
            _tokenHolderAccountFaucet = tokenHolderAccountFaucet;
        }

        public async Task Mint(string contractAddress)
        {
            // ToDo: Gerar arquivo metadata dinamicamente e armazenar em local público (filebase?)
            var mintFunctionMessage = new MintFunction()
            {
                ToAddress = _tokenHolderAccountFaucet.GetPublicAddress(),
                MetadataUri = "ipfs://QmbKxHChGcXbbF8cAbsWaVbiU9Fa3vkDgQRqqyN2eWWs6P"
            };

            var mintHandler = _mainAccountFaucet.GetWeb3ETH().GetContractTransactionHandler<MintFunction>();
            var transactionReceipt = await mintHandler.SendRequestAndWaitForReceiptAsync(contractAddress, mintFunctionMessage);
        }

        public async Task TransferToTicketOwner(string contractAddress, string ticketOwnerAddress, long tokenId)
        {
            var transferFunction = new TransferFunction()
            {
                From = _tokenHolderAccountFaucet.GetPublicAddress(),
                To = ticketOwnerAddress,
                TokenId = tokenId
            };
            
            var transferHandler = _tokenHolderAccountFaucet.GetWeb3ETH().GetContractTransactionHandler<TransferFunction>();
            
            var gasEstimate = await transferHandler.EstimateGasAsync(contractAddress, transferFunction);
            transferFunction.Gas = gasEstimate.Value * 2;
            
            var transactionReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transferFunction);
        }

        public async Task<long> GetBalance(string contractAddress, string ownerAddress)
        {
            var balanceOfFunctionMessage = new BalanceOfFunction()
            {
                Owner = ownerAddress
            };

            var balanceHandler = _mainAccountFaucet.GetWeb3ETH().GetContractQueryHandler<BalanceOfFunction>();
            return await balanceHandler.QueryAsync<long>(contractAddress, balanceOfFunctionMessage);
        }
    }
}