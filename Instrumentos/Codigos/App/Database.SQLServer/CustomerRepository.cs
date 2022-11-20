using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Exceptions;
using Domain.Models.Users;
using Domain.Repositories;

namespace Database.SQLServer
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly DbConnection _dbConnection;

        public CustomerRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<CustomerUser> Insert(CustomerUser user)
        {
            const string sql = @"INSERT INTO User_Customer (Code, Username, Password, FullName, Email, Phone, WalletAddress)
                                                        VALUES (@Code, @Username, @Password, @FullName, @Email, @Phone, @WalletAddress)
                                                        RETURNING *";

            var insertedRow = await _dbConnection.QuerySingle(sql, user);
            if (insertedRow == null)
                throw new RepositoryException($"Error trying to insert customer.");

            return new CustomerUser(
                insertedRow.id,
                insertedRow.code,
                insertedRow.username,
                insertedRow.password,
                insertedRow.fullname,
                insertedRow.email,
                insertedRow.phone,
                insertedRow.walletaddress,
                insertedRow.internaladdress);
        }

        public async Task<CustomerUser> GetByUsername(string username)
        {
            const string sql = @"SELECT *
                                   FROM User_Customer
                                  WHERE Username = @username";

            var customerRow = await _dbConnection.QuerySingle(sql, new
            {
                username
            });

            if (customerRow == null)
                throw new RepositoryException($"Error trying to get customer by username {username}.");

            return new CustomerUser(
                customerRow.id,
                customerRow.code,
                customerRow.username,
                customerRow.password,
                customerRow.fullname,
                customerRow.email,
                customerRow.phone,
                customerRow.walletaddress,
                customerRow.internaladdress);
        }

        public async Task<CustomerUser> GetByCode(string code)
        {
            const string sql = @"SELECT *
                                   FROM User_Customer
                                  WHERE Code = @code";

            var customerRow = await _dbConnection.QuerySingle(sql, new
            {
                code
            });

            if (customerRow == null)
                throw new RepositoryException($"Error trying to get customer by code {code}.");

            return new CustomerUser(
                customerRow.id,
                customerRow.code,
                customerRow.username,
                customerRow.password,
                customerRow.fullname,
                customerRow.email,
                customerRow.phone,
                customerRow.walletaddress,
                customerRow.internaladdress);
        }

        public async Task UpdateInternalAddress(CustomerUser customer)
        {
            const string sql = @"UPDATE User_Customer
                                    SET InternalAddress = @internalAddress
                                  WHERE Code = @code";

            int rowsAffected = await _dbConnection.ExecuteAsyncWithTransaction(sql, new
            {
                internalAddress = customer.InternalAddress,
                code = customer.Code
            });

            if (rowsAffected == 0)
                throw new RepositoryException($"Error trying to update internal address for customer {customer.Code}");
        }

        public async Task<CustomerUser?> GetByInternalAddress(string internalAddress)
        {
            const string sql = @"SELECT *
                                   FROM User_Customer
                                  WHERE InternalAddress = @internalAddress";

            var customerRow = await _dbConnection.QuerySingle(sql, new
            {
                internalAddress
            });

            if (customerRow == null)
                return null;

            return new CustomerUser(
                customerRow.id,
                customerRow.code,
                customerRow.username,
                customerRow.password,
                customerRow.fullname,
                customerRow.email,
                customerRow.phone,
                customerRow.walletaddress,
                customerRow.internaladdress);
        }
    }
}