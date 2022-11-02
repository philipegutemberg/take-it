using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Ethereum.Nethereum.SmartContracts.ERC721Mintable.BlockProcessor
{
    public interface IERC721BlockProcessor
    {
        Task StartProcessing(int minimumBlockConfirmations, Event @event, CancellationToken cancellationToken);
    }
}