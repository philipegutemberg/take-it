using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Ethereum.Nethereum.TicketTokenSmartContract.Functions
{
    [Function("safeMint")]
    internal class MintFunction : FunctionMessage
    {
        [Parameter("address", "to", 1)]
        public string ToAddress { get; set; }
        
        [Parameter("string", "uri", 2)]
        public string MetadataUri { get; set; }
    }
}