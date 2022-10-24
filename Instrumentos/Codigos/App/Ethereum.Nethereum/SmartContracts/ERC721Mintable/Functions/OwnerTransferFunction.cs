using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Ethereum.Nethereum.SmartContracts.ERC721Mintable.Functions
{
    [Function("ownerSafeTransfer")]
    internal class OwnerTransferFunction : FunctionMessage
    {
        [Parameter("address", "from", 1)]
        public string? From { get; set; }

        [Parameter("address", "to", 2)]
        public string? To { get; set; }

        [Parameter("uint256", "tokenId", 3)]
        public BigInteger TokenId { get; set; }
    }
}