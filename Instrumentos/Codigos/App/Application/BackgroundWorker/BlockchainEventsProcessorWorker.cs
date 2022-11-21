using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;
using Ethereum.Nethereum.SmartContracts.ERC721Mintable.BlockProcessor;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.BackgroundWorker
{
    public sealed class BlockchainEventsProcessorWorker : BackgroundService
    {
        private readonly ILogger<BlockchainEventsProcessorWorker> _logger;
        private readonly IERC721BlockProcessor _blockProcessor;
        private readonly IEventRepository _eventRepository;

        public BlockchainEventsProcessorWorker(ILogger<BlockchainEventsProcessorWorker> logger, IERC721BlockProcessor blockProcessor, IEventRepository eventRepository)
        {
            _logger = logger;
            _blockProcessor = blockProcessor;
            _eventRepository = eventRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartWorkWithTimer(stoppingToken, TimeSpan.FromMinutes(5));
        }

        private async Task StartWorkWithTimer(CancellationToken stoppingToken, TimeSpan timeout)
        {
            try
            {
                CancellationTokenSource ctSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
                ctSource.CancelAfter(timeout);

                await DoWork(ctSource.Token);
            }
            catch (OperationCanceledException e)
            {
                if (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation(e, "Timeout operation exceeded. Starting new iteration");
                    await StartWorkWithTimer(stoppingToken, timeout);
                }
                else
                {
                    _logger.LogError(e, "Parent cancellation token cancelled");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unknown error on log processing");
            }
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            var events = await _eventRepository.GetAllEnabled();

            var tasks = events.Select(e => _blockProcessor.StartProcessing(1, e, stoppingToken));

            await Task.WhenAll(tasks);
        }
    }
}