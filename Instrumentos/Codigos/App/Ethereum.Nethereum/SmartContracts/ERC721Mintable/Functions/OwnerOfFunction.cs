using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Ethereum.Nethereum.SmartContracts.ERC721Mintable.Functions
{
    [Function("ownerOf", "address")]
    internal class OwnerOfFunction : FunctionMessage
    {
        [Parameter("uint256", "tokenId", 1)]
        public BigInteger TokenId { get; set; }
    }
}