using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;

namespace Database.SQLServer
{
    internal class TicketRepository : ITicketRepository
    {
        private readonly DbConnection _dbConnection;

        public TicketRepository(DbConnection DbConnection)
        {
            _dbConnection = DbConnection;
        }

        public async Task<Ticket> Insert(Ticket ticket)
        {
            const string sql = @"INSERT INTO dbo.Ticket (Code, EventCode, EventTicketTypeCode, PurchaseDate, OwnerCustomerCode, TokenId)
                                                        OUTPUT INSERTED.*
                                                        VALUES (@Code, @EventCode, @EventTicketTypeCode, @PurchaseDate, @OwnerCustomerCode, @TokenId)";

            var insertedRow = await _dbConnection.QuerySingle(sql, ticket);
            if (insertedRow == null)
                throw new RepositoryException($"Error trying to insert ticket.");

            return new Ticket(
                insertedRow.Code,
                insertedRow.EventCode,
                insertedRow.EventTicketTypeCode,
                insertedRow.PurchaseDate,
                insertedRow.OwnerCustomerCode,
                insertedRow.TokenId);
        }

        public async Task<Ticket> GetByCode(string code)
        {
            const string sql = @"SELECT *
                                   FROM dbo.Ticket
                                  WHERE Code = @code";

            var ticketRow = await _dbConnection.QuerySingle(sql, new
            {
                code
            });

            if (ticketRow == null)
                throw new RepositoryException($"Error trying to get ticket.");

            return new Ticket(
                ticketRow.Code,
                ticketRow.EventCode,
                ticketRow.EventTicketTypeCode,
                ticketRow.PurchaseDate,
                ticketRow.OwnerCustomerCode,
                ticketRow.TokenId);
        }

        public async Task<IEnumerable<Ticket>> GetAllOwnedByCustomer(string customerCode)
        {
            const string ticketSql = @"SELECT *
                                   FROM dbo.Ticket
                                  WHERE OwnerCustomerCode = @customerCode";

            var ticketRows = await _dbConnection.QueryAsync(ticketSql, new
            {
                customerCode = customerCode
            });

            return ticketRows.Select(t => new Ticket(
                t.Code,
                t.EventCode,
                t.EventTicketTypeCode,
                t.PurchaseDate,
                t.OwnerCustomerCode,
                t.TokenId));
        }
    }
}