using Nethereum.HdWallet;

namespace Ethereum.Nethereum.Services
{
    internal class WalletService
    {
        private readonly Wallet _wallet;

        public WalletService(string words, string password)
        {
            _wallet = new Wallet(words, password);
        }

        public Wallet Get() => _wallet;
    }
}