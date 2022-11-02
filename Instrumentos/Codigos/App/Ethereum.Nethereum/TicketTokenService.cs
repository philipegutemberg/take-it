using System.Numerics;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Users;
using Domain.Services.Interfaces;
using Ethereum.Nethereum.Services;
using Ethereum.Nethereum.SmartContracts.ERC721Mintable.Functions;
using Nethereum.Contracts.Standards.ERC721.ContractDefinition;

namespace Ethereum.Nethereum
{
    internal class TicketTokenService : ITokenService
    {
        private readonly AccountService _accountService;
        private readonly OwnerAccountsService _ownerAccountsService;
        private readonly Web3Service _web3Service;

        public TicketTokenService(AccountService accountService, OwnerAccountsService ownerAccountsService, Web3Service web3Service)
        {
            _accountService = accountService;
            _ownerAccountsService = ownerAccountsService;
            _web3Service = web3Service;
        }

        public async Task EmitToken(EventTicketType ticketType, Ticket ticket, Event @event, CustomerUser customer)
        {
            var mintFunctionMessage = new MintFunction()
            {
                ToAddress = _accountService.Get(customer.Id).Address,
                MetadataUri = ticketType.MetadataFileUrl,
                TokenId = ticket.TokenId
            };

            var web3 = _web3Service.GetWeb3(_ownerAccountsService.GetContractOwner());
            var mintHandler = web3.Eth.GetContractTransactionHandler<MintFunction>();
            await mintHandler.SendRequestAndWaitForReceiptAsync(@event.TokenContractAddress, mintFunctionMessage);
        }

        public async Task TransferToCustomer(Ticket ticket, Event @event, CustomerUser customer)
        {
            if (string.IsNullOrWhiteSpace(customer.WalletAddress))
                throw new InvalidAddressException(customer.WalletAddress);

            var transferFunction = new OwnerTransferFunction()
            {
                From = _accountService.Get(customer.Id).Address,
                To = customer.WalletAddress,
                TokenId = ticket.TokenId
            };

            var web3 = _web3Service.GetWeb3(_ownerAccountsService.GetContractOwner());
            var transferHandler = web3.Eth.GetContractTransactionHandler<OwnerTransferFunction>();

            var gasEstimate = await transferHandler.EstimateGasAsync(@event.TokenContractAddress, transferFunction);
            transferFunction.Gas = (BigInteger)((long)gasEstimate.Value * 1.5);

            await transferHandler.SendRequestAndWaitForReceiptAsync(@event.TokenContractAddress, transferFunction);
        }

        public async Task<long> GetCustomerBalance(Event @event, CustomerUser customer)
        {
            var balanceOfFunctionMessage = new BalanceOfFunction()
            {
                Owner = _accountService.Get(customer.Id).Address
            };

            var web3 = _web3Service.GetWeb3(_ownerAccountsService.GetContractOwner());
            var balanceHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
            return await balanceHandler.QueryAsync<long>(@event.TokenContractAddress, balanceOfFunctionMessage);
        }

        public Task<string> GetCustomerInternalAddress(CustomerUser customer)
        {
            return Task.FromResult(_accountService.Get(customer.Id).Address);
        }

        public async Task<bool> CheckCustomerTokenOwnership(Event @event, CustomerUser customer, Ticket ticket)
        {
            var ownerOfFunction = new OwnerOfFunction
            {
                TokenId = ticket.TokenId
            };

            var web3 = _web3Service.GetWeb3(_ownerAccountsService.GetContractOwner());
            var balanceHandler = web3.Eth.GetContractQueryHandler<OwnerOfFunction>();
            string legitOwnerAddress = await balanceHandler.QueryAsync<string>(@event.TokenContractAddress, ownerOfFunction);

            return legitOwnerAddress == _accountService.Get(customer.Id).Address;
        }
    }
}