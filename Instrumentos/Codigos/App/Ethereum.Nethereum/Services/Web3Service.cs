using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Ethereum.Nethereum.Services
{
    internal class Web3Service
    {
        private readonly string _url;

        public Web3Service(string url)
        {
            _url = url;
        }

        public Web3 GetWeb3(Account account) => new Web3(account, _url);
    }
}