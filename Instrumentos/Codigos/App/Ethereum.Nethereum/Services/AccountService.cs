using Nethereum.Web3.Accounts;

namespace Ethereum.Nethereum.Services
{
    internal class AccountService
    {
        private readonly WalletService _walletService;

        public AccountService(WalletService walletService)
        {
            _walletService = walletService;
        }

        public Account Get(int index) => _walletService.Get().GetAccount(index);
    }
}