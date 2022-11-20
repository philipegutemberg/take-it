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
            string sql = @$"DO $$
                            BEGIN
                                IF EXISTS (SELECT 1 FROM EthereumEventProcessing) THEN
                                    UPDATE EthereumEventProcessing
                                    SET LastBlockReadNumber = {lastProcessed};
                                ELSE
                                    INSERT INTO EthereumEventProcessing VALUES ({lastProcessed});
                                END IF;
                            END $$;";

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
                                 FROM EthereumEventProcessing";

            return await _dbConnection.QuerySingle<long>(sql);
        }
    }
}