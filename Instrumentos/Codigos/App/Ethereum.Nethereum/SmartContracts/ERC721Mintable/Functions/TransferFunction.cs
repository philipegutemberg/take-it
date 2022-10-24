using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Ethereum.Nethereum.SmartContracts.ERC721Mintable.Functions
{
    [Function("safeTransferFrom")]
    internal class TransferFunction : FunctionMessage
    {
        [Parameter("address", "_from", 1)]
        public string? From { get; set; }

        [Parameter("address", "_to", 2)]
        public string? To { get; set; }

        [Parameter("uint256", "_tokenId", 3)]
        public BigInteger TokenId { get; set; }
    }
}