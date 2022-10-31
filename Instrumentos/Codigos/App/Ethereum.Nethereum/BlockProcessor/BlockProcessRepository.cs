using System.Numerics;
using System.Threading.Tasks;
using Domain.Repositories;
using Nethereum.BlockchainProcessing.ProgressRepositories;

namespace Ethereum.Nethereum.BlockProcessor
{
    internal class BlockProgressRepository : IBlockProgressRepository
    {
        private readonly ITokenEventProcessingRepository _tokenEventProcessingRepository;

        public BlockProgressRepository(ITokenEventProcessingRepository tokenEventProcessingRepository)
        {
            _tokenEventProcessingRepository = tokenEventProcessingRepository;
        }

        public async Task UpsertProgressAsync(BigInteger blockNumber)
        {
            await _tokenEventProcessingRepository.SetLastProcessed((long)blockNumber);
        }

        public async Task<BigInteger?> GetLastBlockNumberProcessedAsync()
        {
            return await _tokenEventProcessingRepository.GetLastProcessed();
        }
    }
}