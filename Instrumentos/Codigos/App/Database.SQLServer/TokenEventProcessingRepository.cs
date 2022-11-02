using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Exceptions;
using Domain.Repositories;

namespace Database.SQLServer
{
    internal class TokenEventProcessingRepository : ITokenEventProcessingRepository
    {
        private readonly DbConnection _dbConnection;

        public TokenEventProcessingRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task SetLastProcessed(long lastProcessed)
        {
            const string sql = @"IF EXISTS (SELECT 1 FROM dbo.EthereumEventProcessing)
                                BEGIN
                                    UPDATE dbo.EthereumEventProcessing
                                    SET LastBlockReadNumber = @lastProcessed
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO dbo.EthereumEventProcessing VALUES (@lastProcessed)
                                END";

            int affectedRows = await _dbConnection.ExecuteAsyncWithTransaction(sql, new
            {
                lastProcessed
            });

            if (affectedRows == 0)
                throw new RepositoryException($"Error trying to set last processed event token {lastProcessed}.");
        }

        public async Task<long?> GetLastProcessed()
        {
            const string sql = @"SELECT LastBlockReadNumber 
                                 FROM dbo.EthereumEventProcessing";

            return await _dbConnection.QuerySingle<long>(sql);
        }
    }
}