using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;

namespace Database.SQLServer
{
    internal class EventTicketTypeRepository : IEventTicketTypeRepository
    {
        private readonly DbConnection _dbConnection;

        public EventTicketTypeRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<EventTicketType> Save(EventTicketType eventTicketType)
        {
            const string sql = @"INSERT INTO dbo.[EventTicketType] (Code, EventCode, TicketName, StartDate, EndDate, Qualification, PriceBrl, MetadataFileUrl, TotalAvailableTickets, CurrentlyAvailableTickets)
                                                        OUTPUT INSERTED.*
                                                        VALUES (@Code, @EventCode, @TicketName, @StartDate, @EndDate, @Qualification, @PriceBrl, @MetadataFileUrl, @TotalAvailableTickets, @CurrentlyAvailableTickets)";

            var insertedRow = await _dbConnection.QuerySingle(sql, new
            {
                eventTicketType.Code,
                eventTicketType.EventCode,
                eventTicketType.TicketName,
                eventTicketType.StartDate,
                eventTicketType.EndDate,
                eventTicketType.Qualification,
                eventTicketType.PriceBrl,
                eventTicketType.MetadataFileUrl,
                eventTicketType.TicketStock.TotalAvailableTickets,
                eventTicketType.TicketStock.CurrentlyAvailableTickets
            });

            if (insertedRow == null)
                throw new RepositoryException($"Error trying to insert event ticket type.");

            return new EventTicketType(
                insertedRow.Code,
                insertedRow.EventCode,
                insertedRow.TicketName,
                insertedRow.StartDate,
                insertedRow.EndDate,
                insertedRow.Qualification,
                insertedRow.PriceBrl,
                insertedRow.MetadataFileUrl,
                new EventTicketTypeStock(
                    insertedRow.Code,
                    insertedRow.EventCode,
                    insertedRow.TotalAvailableTickets,
                    insertedRow.CurrentlyAvailableTickets
                ));
        }

        public async Task<EventTicketType> GetByCode(string code)
        {
            const string sql = @"SELECT *
                                   FROM dbo.[EventTicketType]
                                  WHERE Code = @code";

            var eventTicketTypeRow = await _dbConnection.QuerySingle(sql, new
            {
                code
            });

            if (eventTicketTypeRow == null)
                throw new RepositoryException($"Error trying to get event ticket type.");

            return new EventTicketType(
                eventTicketTypeRow.Code,
                eventTicketTypeRow.EventCode,
                eventTicketTypeRow.TicketName,
                eventTicketTypeRow.StartDate,
                eventTicketTypeRow.EndDate,
                eventTicketTypeRow.Qualification,
                eventTicketTypeRow.PriceBrl,
                eventTicketTypeRow.MetadataFileUrl,
                new EventTicketTypeStock(
                    eventTicketTypeRow.Code,
                    eventTicketTypeRow.EventCode,
                    eventTicketTypeRow.TotalAvailableTickets,
                    eventTicketTypeRow.CurrentlyAvailableTickets
                ));
        }

        public async Task<IEnumerable<EventTicketType>> GetAllByEvent(string eventCode)
        {
            const string sql = @"SELECT *
                                   FROM dbo.[EventTicketType]
                                  WHERE EventCode = @eventCode";

            var eventTicketTypeRows = await _dbConnection.QueryAsync(sql, new
            {
                eventCode
            });

            if (eventTicketTypeRows == null)
                throw new RepositoryException($"Error trying to get available event ticket types for event {eventCode}.");

            return eventTicketTypeRows.Select(e => new EventTicketType(
                e.Code,
                e.EventCode,
                e.TicketName,
                e.StartDate,
                e.EndDate,
                e.Qualification,
                e.PriceBrl,
                e.MetadataFileUrl,
                new EventTicketTypeStock(
                    e.Code,
                    e.EventCode,
                    e.TotalAvailableTickets,
                    e.CurrentlyAvailableTickets
                )));
        }

        public async Task<List<string>> GetAllByEvent_Codes(string eventCode)
        {
            const string sql = @"SELECT Code
                                   FROM dbo.[EventTicketType]
                                  WHERE EventCode = @eventCode";

            var eventTicketTypeCodes = await _dbConnection.QueryAsync<string>(sql, new
            {
                eventCode
            });

            if (eventTicketTypeCodes == null)
                throw new RepositoryException($"Error trying to get event ticket type by event {eventCode}.");

            return eventTicketTypeCodes.ToList();
        }
    }
}