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
            const string sql = @"INSERT INTO Event (Code, StartDate, EndDate, Location, Title, Description, Ticker, TokenContractAddress, ImageUrl, ResaleFeePercentage, AlreadyIssuedTickets)
                                                        VALUES (@Code, @StartDate, @EndDate, @Location, @Title, @Description, @Ticker, @TokenContractAddress, @ImageUrl, @ResaleFeePercentage, @AlreadyIssuedTickets)
                                                        RETURNING *";

            var insertedRow = await _dbConnection.QuerySingle(sql, newEvent);
            if (insertedRow == null)
                throw new RepositoryException($"Error trying to insert event.");

            return new Event(
                insertedRow.code,
                insertedRow.startdate,
                insertedRow.enddate,
                insertedRow.location,
                insertedRow.title,
                insertedRow.description,
                insertedRow.ticker,
                insertedRow.tokencontractaddress,
                insertedRow.imageurl,
                insertedRow.resalefeepercentage,
                insertedRow.alreadyissuedtickets);
        }

        public async Task<Event> GetByCode(string code)
        {
            const string sql = @"SELECT *
                                   FROM Event
                                  WHERE Code = @code";

            var eventRow = await _dbConnection.QuerySingle(sql, new
            {
                code
            });

            if (eventRow == null)
                throw new RepositoryException($"Error trying to get event.");

            return new Event(
                eventRow.code,
                eventRow.startdate,
                eventRow.enddate,
                eventRow.location,
                eventRow.title,
                eventRow.description,
                eventRow.ticker,
                eventRow.tokencontractaddress,
                eventRow.imageurl,
                eventRow.resalefeepercentage,
                eventRow.alreadyissuedtickets);
        }

        public async Task<IEnumerable<Event>> GetAllEnabled()
        {
            const string sql = @"SELECT *
                                   FROM Event
                                  WHERE EndDate >= CURRENT_DATE";

            var eventsRows = await _dbConnection.QueryAsync(sql);

            if (eventsRows == null)
                throw new RepositoryException($"Error trying to get all enabled events.");

            return eventsRows.Select(e => new Event(
                e.code,
                e.startdate,
                e.enddate,
                e.location,
                e.title,
                e.description,
                e.ticker,
                e.tokencontractaddress,
                e.imageurl,
                e.resalefeepercentage,
                e.alreadyissuedtickets));
        }

        public async Task UpdateIssuedTickets(string eventCode, long alreadyIssuedTickets)
        {
            const string sql = @"UPDATE Event
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