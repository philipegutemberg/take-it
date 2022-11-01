using System.Collections.Generic;
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

        public async Task<Customer> Insert(Customer user)
        {
            const string sql = @"INSERT INTO dbo.User_Customer (Code, Username, Password, FullName, Email, Phone, WalletAddress)
                                                        OUTPUT INSERTED.*
                                                        VALUES (@Code, @Username, @Password, @FullName, @Email, @Phone, @WalletAddress)";

            var insertedRow = await _dbConnection.QuerySingle(sql, user);
            if (insertedRow == null)
                throw new RepositoryException($"Error trying to insert customer.");

            return new Customer(
                insertedRow.Id,
                insertedRow.Code,
                insertedRow.Username,
                insertedRow.Password,
                insertedRow.FullName,
                insertedRow.Email,
                insertedRow.Phone,
                insertedRow.WalletAddress,
                insertedRow.InternalAddress);
        }

        public async Task<Customer> GetByUsername(string username)
        {
            const string sql = @"SELECT *
                                   FROM dbo.User_Customer
                                  WHERE Username = @username";

            var customerRow = await _dbConnection.QuerySingle(sql, new
            {
                username
            });

            if (customerRow == null)
                throw new RepositoryException($"Error trying to get customer by username {username}.");

            return new Customer(
                customerRow.Id,
                customerRow.Code,
                customerRow.Username,
                customerRow.Password,
                customerRow.FullName,
                customerRow.Email,
                customerRow.Phone,
                customerRow.WalletAddress,
                customerRow.InternalAddress);
        }

        public async Task<Customer> GetByCode(string code)
        {
            const string sql = @"SELECT *
                                   FROM dbo.User_Customer
                                  WHERE Code = @code";

            var customerRow = await _dbConnection.QuerySingle(sql, new
            {
                code
            });

            if (customerRow == null)
                throw new RepositoryException($"Error trying to get customer by code {code}.");

            return new Customer(
                customerRow.Id,
                customerRow.Code,
                customerRow.Username,
                customerRow.Password,
                customerRow.FullName,
                customerRow.Email,
                customerRow.Phone,
                customerRow.WalletAddress,
                customerRow.InternalAddress);
        }

        public async Task UpdateInternalAddress(Customer customer)
        {
            const string sql = @"UPDATE dbo.User_Customer
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

        public async Task<Customer?> GetByInternalAddress(string internalAddress)
        {
            const string sql = @"SELECT *
                                   FROM dbo.User_Customer
                                  WHERE InternalAddress = @internalAddress";

            var customerRow = await _dbConnection.QuerySingle(sql, new
            {
                internalAddress
            });

            if (customerRow == null)
                return null;

            return new Customer(
                customerRow.Id,
                customerRow.Code,
                customerRow.Username,
                customerRow.Password,
                customerRow.FullName,
                customerRow.Email,
                customerRow.Phone,
                customerRow.WalletAddress,
                customerRow.InternalAddress);
        }
    }
}