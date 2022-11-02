using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services.Interfaces;
using Ethereum.Nethereum.Services;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.Contracts.Standards.ERC721.ContractDefinition;

namespace Ethereum.Nethereum.SmartContracts.ERC721Mintable.BlockProcessor
{
    internal class ERC721BlockProcessor : IERC721BlockProcessor
    {
        private readonly Web3Service _web3Service;
        private readonly ITokenLogProcessingService _tokenLogProcessingService;
        private readonly IBlockProgressRepository _blockProgressRepository;

        public ERC721BlockProcessor(
            Web3Service web3Service,
            ITokenLogProcessingService tokenLogProcessingService,
            IBlockProgressRepository blockProgressRepository)
        {
            _web3Service = web3Service;
            _tokenLogProcessingService = tokenLogProcessingService;
            _blockProgressRepository = blockProgressRepository;
        }

        public async Task StartProcessing(int minimumBlockConfirmations, Event @event, CancellationToken cancellationToken)
        {
            // https://docs.nethereum.com/en/latest/nethereum-log-processing-detail/
            var processor = _web3Service.GetWeb3().Processing.Logs.CreateProcessorForContract<TransferEventDTO>(
                contractAddress: @event.TokenContractAddress,
                action: eventLog => _tokenLogProcessingService.ProcessEventLog(eventLog.Event.From, eventLog.Event.To, (long)eventLog.Event.TokenId),
                blockProgressRepository: _blockProgressRepository,
                minimumBlockConfirmations: (uint)minimumBlockConfirmations);

            await processor.ExecuteAsync(
                cancellationToken,
                startAtBlockNumberIfNotProcessed: new BigInteger(7860000));
        }
    }
}