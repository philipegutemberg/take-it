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
            const string sql = @"INSERT INTO Ticket (Code, EventCode, EventTicketTypeCode, PurchaseDate, OwnerCustomerCode, TokenId, UsedOnEvent)
                                                        VALUES (@Code, @EventCode, @EventTicketTypeCode, @PurchaseDate, @OwnerCustomerCode, @TokenId, @UsedOnEvent)
                                                        RETURNING *";

            var insertedRow = await _dbConnection.QuerySingle(sql, ticket);
            if (insertedRow == null)
                throw new RepositoryException($"Error trying to insert ticket.");

            return new Ticket(
                insertedRow.code,
                insertedRow.eventcode,
                insertedRow.eventtickettypecode,
                insertedRow.purchasedate,
                insertedRow.ownercustomercode,
                insertedRow.tokenid,
                insertedRow.usedonevent);
        }

        public async Task<Ticket> GetByCode(string code)
        {
            const string sql = @"SELECT *
                                   FROM Ticket
                                  WHERE Code = @code";

            var ticketRow = await _dbConnection.QuerySingle(sql, new
            {
                code
            });

            if (ticketRow == null)
                throw new RepositoryException($"Error trying to get ticket.");

            return new Ticket(
                ticketRow.code,
                ticketRow.eventcode,
                ticketRow.eventtickettypecode,
                ticketRow.purchasedate,
                ticketRow.ownercustomercode,
                ticketRow.tokenid,
                ticketRow.usedonevent);
        }

        public async Task<IEnumerable<Ticket>> GetAllOwnedByCustomer(string customerCode)
        {
            const string ticketSql = @"SELECT *
                                   FROM Ticket
                                  WHERE OwnerCustomerCode = @customerCode";

            var ticketRows = await _dbConnection.QueryAsync(ticketSql, new
            {
                customerCode = customerCode
            });

            return ticketRows.Select(t => new Ticket(
                t.code,
                t.eventcode,
                t.eventtickettypecode,
                t.purchasedate,
                t.ownercustomercode,
                t.tokenid,
                t.usedonevent));
        }

        public async Task<Ticket> GetByTokenId(string eventCode, long tokenId)
        {
            const string sql = @"SELECT *
                                   FROM Ticket
                                  WHERE TokenId = @tokenId
                                  AND EventCode = @eventCode";

            var ticketRow = await _dbConnection.QuerySingle(sql, new
            {
                tokenId,
                eventCode
            });

            if (ticketRow == null)
                throw new RepositoryException($"Error trying to get ticket by token id {tokenId}.");

            return new Ticket(
                ticketRow.code,
                ticketRow.eventcode,
                ticketRow.eventtickettypecode,
                ticketRow.purchasedate,
                ticketRow.ownercustomercode,
                ticketRow.tokenid,
                ticketRow.usedonevent);
        }

        public async Task UpdateOwner(Ticket ticket)
        {
            const string sql = @"UPDATE Ticket
                                    SET OwnerCustomerCode = @customerOwner
                                  WHERE Code = @code";

            int rowsAffected = await _dbConnection.ExecuteAsyncWithTransaction(sql, new
            {
                customerOwner = ticket.OwnerCustomerCode,
                code = ticket.Code
            });

            if (rowsAffected == 0)
                throw new RepositoryException($"Error trying to update owner for ticket {ticket.Code}.");
        }

        public async Task UpdateUsedOnEvent(Ticket ticket)
        {
            const string sql = @"UPDATE Ticket
                                    SET UsedOnEvent = @usedOnEvent
                                  WHERE Code = @code
                                    AND OwnerCustomerCode = @ownerCustomerCode
                                    AND UsedOnEvent = @filterUsedOnEvent";

            int rowsAffected = await _dbConnection.ExecuteAsyncWithTransaction(sql, new
            {
                usedOnEvent = ticket.UsedOnEvent,
                code = ticket.Code,
                ownerCustomerCode = ticket.OwnerCustomerCode,
                filterUsedOnEvent = !ticket.UsedOnEvent
            });

            if (rowsAffected == 0)
                throw new RepositoryException($"Error trying to update used on event for ticket {ticket.Code}.");
        }
    }
}