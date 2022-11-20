using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Exceptions;
using Domain.Models.Users;
using Domain.Repositories;

namespace Database.SQLServer
{
    internal class GatekeeperUserRepository : IGatekeeperUserRepository
    {
        private readonly DbConnection _dbConnection;

        public GatekeeperUserRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<GatekeeperUser> Insert(GatekeeperUser user)
        {
            const string sql = @"INSERT INTO User_Gatekeeper (Code, Username, Password)
                                                        VALUES (@Code, @Username, @Password)
                                                        RETURNING *";

            var insertedRow = await _dbConnection.QuerySingle(sql, user);
            if (insertedRow == null)
                throw new RepositoryException($"Error trying to insert gatekeeper user.");

            return new GatekeeperUser(
                insertedRow.id,
                insertedRow.code,
                insertedRow.username,
                insertedRow.password);
        }
    }
}