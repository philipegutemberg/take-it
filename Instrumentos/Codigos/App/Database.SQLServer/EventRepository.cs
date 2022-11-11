using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;

namespace Database.SQLServer
{
    internal class EventRepository : IEventRepository
    {
        private readonly DbConnection _dbConnection;

        public EventRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Event> Insert(Event newEvent)
        {
            const string sql = @"INSERT INTO dbo.[Event] (Code, StartDate, EndDate, [Location], Title, [Description], Ticker, TokenContractAddress, ImageUrl, ResaleFeePercentage, AlreadyIssuedTickets)
                                                        OUTPUT INSERTED.*
                                                        VALUES (@Code, @StartDate, @EndDate, @Location, @Title, @Description, @Ticker, @TokenContractAddress, @ImageUrl, @ResaleFeePercentage, @AlreadyIssuedTickets)";

            var insertedRow = await _dbConnection.QuerySingle(sql, newEvent);
            if (insertedRow == null)
                throw new RepositoryException($"Error trying to insert event.");

            return new Event(
                insertedRow.Code,
                insertedRow.StartDate,
                insertedRow.EndDate,
                insertedRow.Location,
                insertedRow.Title,
                insertedRow.Description,
                insertedRow.Ticker,
                insertedRow.TokenContractAddress,
                insertedRow.ImageUrl,
                insertedRow.ResaleFeePercentage,
                insertedRow.AlreadyIssuedTickets);
        }

        public async Task<Event> GetByCode(string code)
        {
            const string sql = @"SELECT *
                                   FROM dbo.[Event]
                                  WHERE Code = @code";

            var eventRow = await _dbConnection.QuerySingle(sql, new
            {
                code
            });

            if (eventRow == null)
                throw new RepositoryException($"Error trying to get event.");

            return new Event(
                eventRow.Code,
                eventRow.StartDate,
                eventRow.EndDate,
                eventRow.Location,
                eventRow.Title,
                eventRow.Description,
                eventRow.Ticker,
                eventRow.TokenContractAddress,
                eventRow.ImageUrl,
                eventRow.ResaleFeePercentage,
                eventRow.AlreadyIssuedTickets);
        }

        public async Task<IEnumerable<Event>> GetAllEnabled()
        {
            const string sql = @"SELECT *
                                   FROM dbo.[Event]
                                  WHERE EndDate >= GETDATE()";

            var eventsRows = await _dbConnection.QueryAsync(sql);

            if (eventsRows == null)
                throw new RepositoryException($"Error trying to get all enabled events.");

            return eventsRows.Select(e => new Event(
                e.Code,
                e.StartDate,
                e.EndDate,
                e.Location,
                e.Title,
                e.Description,
                e.Ticker,
                e.TokenContractAddress,
                e.ImageUrl,
                e.ResaleFeePercentage,
                e.AlreadyIssuedTickets));
        }

        public async Task UpdateIssuedTickets(string eventCode, long alreadyIssuedTickets)
        {
            const string sql = @"UPDATE dbo.[Event]
                                    SET AlreadyIssuedTickets = @alreadyIssuedTickets
                                  WHERE Code = @code";

            int rowsAffected = await _dbConnection.ExecuteAsyncWithTransaction(sql, new
            {
                alreadyIssuedTickets,
                code = eventCode
            });

            if (rowsAffected == 0)
                throw new RepositoryException($"Error trying to update already issued tickets for event {eventCode}");
        }
    }
}