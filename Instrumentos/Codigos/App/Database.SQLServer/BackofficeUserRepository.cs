using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Exceptions;
using Domain.Models.Users;
using Domain.Repositories;

namespace Database.SQLServer
{
    internal class BackofficeUserRepository : IBackofficeUserRepository
    {
        private readonly DbConnection _dbConnection;

        public BackofficeUserRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<BackOfficeUser> Insert(BackOfficeUser user)
        {
            const string sql = @"INSERT INTO User_Backoffice (Code, Username, Password)
                                                        VALUES (@Code, @Username, @Password)
                                                        RETURNING *";

            var insertedRow = await _dbConnection.QuerySingle(sql, user);
            if (insertedRow == null)
                throw new RepositoryException($"Error trying to insert backoffice user.");

            return new BackOfficeUser(
                insertedRow.id,
                insertedRow.code,
                insertedRow.username,
                insertedRow.password);
        }
    }
}