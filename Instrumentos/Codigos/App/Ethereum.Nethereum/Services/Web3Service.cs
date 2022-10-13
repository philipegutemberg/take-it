using Ethereum.Nethereum.Services.Interfaces;
using Nethereum.Contracts.Services;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Ethereum.Nethereum.Services
{
    internal class Web3Service : IMainAccountFaucet, ITokenHolderAccountFaucet
    {
        private readonly Web3 _web3;
        private readonly string _accountPublicKey;
        
        public Web3Service(string url, string accountPrivateKey, string accountPublicKey)
        {
            var account = new Account(accountPrivateKey);
            _web3 = new Web3(account, url);

            _accountPublicKey = accountPublicKey;
        }

        public Web3 GetWeb3() => _web3;

        public IEthApiContractService GetWeb3ETH() => GetWeb3().Eth;

        public string GetPublicAddress() => _accountPublicKey;
    }
}