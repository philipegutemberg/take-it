using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Ethereum.Nethereum.SmartContracts.ERC721Mintable.Events
{
    [Event("Transfer")]
    internal class TransferEventDTO : IEventDTO
    {
        [Parameter("address", "_from", 1, true)]
        public string From { get; set; }

        [Parameter("address", "_to", 2, true)]
        public string To { get; set; }

        [Parameter("uint256", "_value", 3, false)]
        public BigInteger Value { get; set; }
    }
}