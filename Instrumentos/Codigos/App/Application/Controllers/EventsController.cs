using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Controllers.Base;
using Application.Controllers.Models;
using Domain.Exceptions;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventsController : BaseController
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventService _eventService;
        private readonly ITicketBuyService _ticketBuyService;
        private readonly ITokenTransferService _tokenTransferService;

        public EventsController(ILogger<EventsController> logger, IEventService eventService, ITicketBuyService ticketBuyService, ITokenTransferService tokenTransferService)
        {
            _logger = logger;
            _eventService = eventService;
            _ticketBuyService = ticketBuyService;
            _tokenTransferService = tokenTransferService;
        }

        [HttpPost]
        [Authorize(Roles = "Backoffice")]
        public async Task<IActionResult> Create([FromBody]EventModel newEvent)
        {
            try
            {
                var @event = new Event(
                    newEvent.StartDate,
                    newEvent.EndDate,
                    newEvent.Location!,
                    newEvent.Title!,
                    newEvent.Description!,
                    newEvent.Ticker!,
                    newEvent.ImageUrl!,
                    newEvent.ResaleFeePercentage!);

                var eventTypes = newEvent.TicketTypes!.Select(et => new EventTicketType(
                    @event,
                    et.TicketName!,
                    et.StartDate,
                    et.EndDate,
                    et.Qualification,
                    et.PriceBrl,
                    et.TotalTickets)).ToList();

                await _eventService.Register(@event, eventTypes);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating event");
                return Problem("Error creating event");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Event>>> ListAllAvailable()
        {
            try
            {
                var events = await _eventService.ListAllAvailable();

                return Ok(events.Select(e => new EventModel
                {
                    Description = e.Key.Description,
                    Location = e.Key.Location,
                    Ticker = e.Key.Ticker,
                    Title = e.Key.Title,
                    EndDate = e.Key.EndDate,
                    ImageUrl = e.Key.ImageUrl,
                    ResaleFeePercentage = e.Key.ResaleFeePercentage,
                    StartDate = e.Key.StartDate,
                    TicketTypes = e.Value.Select(tt => new TicketTypesModel
                    {
                        Code = tt.Code,
                        Qualification = tt.Qualification,
                        EndDate = tt.EndDate,
                        PriceBrl = tt.PriceBrl,
                        StartDate = tt.StartDate,
                        TicketName = tt.TicketName,
                        TotalTickets = 0
                    }).ToList()
                }));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error listing available events");
                return Problem("Error listing available events");
            }
        }

        [HttpPost("buy")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> BuyTicket([FromBody]TicketBuyModel ticketBuyModel)
        {
            try
            {
                await _ticketBuyService.Buy(GetLoggedUsername(), ticketBuyModel.EventTicketTypeCode!);

                return Ok();
            }
            catch (UnsuccessfulPurchaseException)
            {
                return StatusCode((int)HttpStatusCode.Conflict);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error buying tickets for event");
                return Problem("Error buying tickets for event");
            }
        }

        [HttpPost("transfer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> TransferTokenToTicketOwner([FromBody]TicketTransferModel ticketTransferModel)
        {
            try
            {
                await _tokenTransferService.Transfer(GetLoggedUsername(), ticketTransferModel.TicketCode!);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error transferring token to ticket owner");
                return Problem("Error transferring token to ticket owner");
            }
        }
    }
}