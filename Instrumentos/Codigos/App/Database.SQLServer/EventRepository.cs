using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;

namespace Database.SQLServer
{
    internal class EventRepository : IEventRepository
    {
        private readonly DbConnection _dbConnection;
        private readonly EventTicketTypeRepository _eventTicketTypeRepository;

        public EventRepository(DbConnection dbConnection, EventTicketTypeRepository eventTicketTypeRepository)
        {
            _dbConnection = dbConnection;
            _eventTicketTypeRepository = eventTicketTypeRepository;
        }

        public async Task<Event> Save(Event newEvent)
        {
            const string sql = @"INSERT INTO dbo.[Event] (Code, StartDate, EndDate, [Location], Title, [Description], Ticker, TokenContractAddress, ImageUrl, AlreadyIssuesTickets)
                                                        OUTPUT INSERTED.*
                                                        VALUES (@Code, @StartDate, @EndDate, @Location, @Title, @Description, @Ticker, @TokenContractAddress, @ImageUrl, @AlreadyIssuedTickets)";

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
                new List<string>(),
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

            var eventTypeCodes = await _eventTicketTypeRepository.GetAllByEvent_Codes(eventRow.Code);

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
                eventTypeCodes,
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

            var eventsRowsEnumerable = eventsRows as dynamic[] ?? eventsRows.ToArray();
            eventsRowsEnumerable.ToList().ForEach(GetEventTypeCodesForEvent);

            return eventsRowsEnumerable.Select(e => new Event(
                e.Code,
                e.StartDate,
                e.EndDate,
                e.Location,
                e.Title,
                e.Description,
                e.Ticker,
                e.TokenContractAddress,
                e.ImageUrl,
                e.EventTypeCodes,
                e.AlreadyIssuedTickets));
        }

        private async void GetEventTypeCodesForEvent(dynamic e)
        {
            e.EventTypeCodes = await _eventTicketTypeRepository.GetAllByEvent_Codes(e.Code);
        }
    }
}