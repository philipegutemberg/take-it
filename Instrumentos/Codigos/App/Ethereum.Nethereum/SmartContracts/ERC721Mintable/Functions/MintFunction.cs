using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Ethereum.Nethereum.SmartContracts.ERC721Mintable.Functions
{
    [Function("safeMint")]
    internal class MintFunction : FunctionMessage
    {
        [Parameter("address", "to", 1)]
        public string? ToAddress { get; set; }

        [Parameter("uint256", "tokenId", 2)]
        public BigInteger TokenId { get; set; }

        [Parameter("string", "uri", 3)]
        public string? MetadataUri { get; set; }
    }
}