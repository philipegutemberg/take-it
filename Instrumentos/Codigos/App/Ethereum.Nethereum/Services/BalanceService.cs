using System.Threading.Tasks;
using Nethereum.Web3;

namespace Ethereum.Nethereum.Services
{
    internal class BalanceService
    {
        private readonly Web3Service _web3Service;

        public BalanceService(Web3Service web3Service)
        {
            _web3Service = web3Service;
        }

        public async Task<decimal> GetBalance(string address)
        {
            var web3 = _web3Service.GetWeb3();
            var balance = await web3.Eth.GetBalance.SendRequestAsync(address);
            return Web3.Convert.FromWei(balance.Value);
        }
    }
}