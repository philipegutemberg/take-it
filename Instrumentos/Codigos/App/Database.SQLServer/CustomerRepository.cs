using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Exceptions;
using Domain.Models.Users;
using Domain.Repositories;

namespace Database.SQLServer
{
    internal class CustomerRepository : IUserRepository<Customer>
    {
        private readonly DbConnection _dbConnection;
        private readonly TicketRepository _ticketRepository;

        public CustomerRepository(DbConnection dbConnection, TicketRepository ticketRepository)
        {
            _dbConnection = dbConnection;
            _ticketRepository = ticketRepository;
        }

        public async Task<Customer> Save(Customer user)
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
                new List<string>());
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
                throw new RepositoryException($"Error trying to get customer.");

            var ticketsOwnedByCustomer = await _ticketRepository.GetAllOwnedByCustomer_Codes(customerRow.Code);

            return new Customer(
                customerRow.Id,
                customerRow.Code,
                customerRow.Username,
                customerRow.Password,
                customerRow.FullName,
                customerRow.Email,
                customerRow.Phone,
                customerRow.WalletAddress,
                ticketsOwnedByCustomer);
        }
    }
}