using Nethereum.Web3.Accounts;

namespace Ethereum.Nethereum.Services
{
    internal class OwnerAccountsService
    {
        private readonly AccountService _accountService;

        public OwnerAccountsService(AccountService accountService)
        {
            _accountService = accountService;
        }

        public Account GetContractOwner() => _accountService.Get(int.MaxValue);
    }
}