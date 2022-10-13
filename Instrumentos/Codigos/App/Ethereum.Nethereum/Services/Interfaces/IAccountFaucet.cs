using Nethereum.Contracts.Services;
using Nethereum.Web3;

namespace Ethereum.Nethereum.Services.Interfaces
{
    public interface IAccountFaucet
    {
        Web3 GetWeb3();
        
        IEthApiContractService GetWeb3ETH();

        string GetPublicAddress();
    }
}