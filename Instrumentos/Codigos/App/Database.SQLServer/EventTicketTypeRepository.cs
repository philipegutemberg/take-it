using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.SQLServer.Connection;
using Domain.Enums;
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

        public async Task<EventTicketType> Insert(EventTicketType eventTicketType)
        {
            const string sql = @"INSERT INTO EventTicketType (Code, EventCode, TicketName, StartDate, EndDate, Qualification, PriceBrl, MetadataFileUrl, TotalAvailableTickets, CurrentlyAvailableTickets)
                                                        VALUES (@Code, @EventCode, @TicketName, @StartDate, @EndDate, @Qualification, @PriceBrl, @MetadataFileUrl, @TotalAvailableTickets, @CurrentlyAvailableTickets)
                                                        RETURNING *";

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
                insertedRow.code,
                insertedRow.eventcode,
                insertedRow.ticketname,
                insertedRow.startdate,
                insertedRow.enddate,
                (EnumTicketQualification)insertedRow.qualification,
                insertedRow.pricebrl,
                insertedRow.metadatafileurl,
                new EventTicketTypeStock(
                    insertedRow.code,
                    insertedRow.eventcode,
                    insertedRow.totalavailabletickets,
                    insertedRow.currentlyavailabletickets
                ));
        }

        public async Task<EventTicketType> GetByCode(string code)
        {
            const string sql = @"SELECT *
                                   FROM EventTicketType
                                  WHERE Code = @code";

            var eventTicketTypeRow = await _dbConnection.QuerySingle(sql, new
            {
                code
            });

            if (eventTicketTypeRow == null)
                throw new RepositoryException($"Error trying to get event ticket type.");

            return new EventTicketType(
                eventTicketTypeRow.code,
                eventTicketTypeRow.eventcode,
                eventTicketTypeRow.ticketname,
                eventTicketTypeRow.startdate,
                eventTicketTypeRow.enddate,
                (EnumTicketQualification)eventTicketTypeRow.qualification,
                eventTicketTypeRow.pricebrl,
                eventTicketTypeRow.metadatafileurl,
                new EventTicketTypeStock(
                    eventTicketTypeRow.code,
                    eventTicketTypeRow.eventcode,
                    eventTicketTypeRow.totalavailabletickets,
                    eventTicketTypeRow.currentlyavailabletickets
                ));
        }

        public async Task<IEnumerable<EventTicketType>> GetAllByEvent(string eventCode)
        {
            const string sql = @"SELECT *
                                   FROM EventTicketType
                                  WHERE EventCode = @eventCode";

            var eventTicketTypeRows = await _dbConnection.QueryAsync(sql, new
            {
                eventCode
            });

            if (eventTicketTypeRows == null)
                throw new RepositoryException($"Error trying to get available event ticket types for event {eventCode}.");

            return eventTicketTypeRows.Select(e => new EventTicketType(
                e.code,
                e.eventcode,
                e.ticketname,
                e.startdate,
                e.enddate,
                (EnumTicketQualification)e.qualification,
                e.pricebrl,
                e.metadatafileurl,
                new EventTicketTypeStock(
                    e.code,
                    e.eventcode,
                    e.totalavailabletickets,
                    e.currentlyavailabletickets
                )));
        }
    }
}