using System;
using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Models.Users;
using Domain.Repositories;

namespace Database.SQLServer
{
    internal class GenericUserRepository : IGenericUserRepository<GenericUser>
    {
        private readonly DbConnection _dbConnection;

        public GenericUserRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<GenericUser> GetByUsername(string username)
        {
            const string sql = @"SELECT Id, Code, Username, Password, 'Customer' as Role
                                   FROM dbo.User_Customer
                                  WHERE Username = @username
                                  
                                  UNION
                                  
                                 SELECT Id, Code, Username, Password, 'Backoffice' as Role
                                   FROM dbo.User_Backoffice
                                  WHERE Username = @username
                                  
                                  UNION

                                 SELECT Id, Code, Username, Password, 'Gatekeeper' as Role
                                   FROM dbo.User_Gatekeeper
                                  WHERE Username = @username";

            var customerRow = await _dbConnection.QuerySingle(sql, new
            {
                username
            });

            if (customerRow == null)
                throw new RepositoryException($"Error trying to get user by username {username}.");

            return new GenericUser(
                customerRow.Id,
                customerRow.Code,
                customerRow.Username,
                customerRow.Password,
                Enum.Parse(typeof(EnumUserRole), customerRow.Role));
        }
    }
}