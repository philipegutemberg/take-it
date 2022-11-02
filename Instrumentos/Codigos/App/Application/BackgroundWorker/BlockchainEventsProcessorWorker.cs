using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;
using Ethereum.Nethereum.SmartContracts.ERC721Mintable.BlockProcessor;
using Microsoft.Extensions.Hosting;

namespace Application.BackgroundWorker
{
    public sealed class BlockchainEventsProcessorWorker : BackgroundService
    {
        private readonly IERC721BlockProcessor _blockProcessor;
        private readonly IEventRepository _eventRepository;

        public BlockchainEventsProcessorWorker(IERC721BlockProcessor blockProcessor, IEventRepository eventRepository)
        {
            _blockProcessor = blockProcessor;
            _eventRepository = eventRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var events = await _eventRepository.GetAllEnabled();

            var tasks = events.Select(e => _blockProcessor.StartProcessing(e, stoppingToken));

            await Task.WhenAll(tasks);
        }
    }
}