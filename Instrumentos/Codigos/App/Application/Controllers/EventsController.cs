using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Controllers.Base;
using Application.Controllers.Models;
using Domain.Exceptions;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventsController : BaseController
    {
        private readonly IEventService _eventService;
        private readonly ITicketBuyService _ticketBuyService;
        private readonly ITokenTransferService _tokenTransferService;

        public EventsController(IEventService eventService, ITicketBuyService ticketBuyService, ITokenTransferService tokenTransferService)
        {
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
                await _eventService.Register(new Event(
                    newEvent.Date,
                    newEvent.Location,
                    newEvent.Title,
                    newEvent.Description,
                    newEvent.Ticker,
                    newEvent.Price,
                    newEvent.TicketsMaxCount
                ));

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Event>>> ListAllAvailable()
        {
            try
            {
                var events = await _eventService.ListAllAvailable();

                return Ok(events);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPost("buy")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> BuyTicket([FromBody]TicketBuyModel ticketBuyModel)
        {
            try
            {
                await _ticketBuyService.Buy(GetLoggedUsername(), ticketBuyModel.EventId);

                return Ok();
            }
            catch (UnsuccessfulPurchaseException)
            {
                return StatusCode((int)HttpStatusCode.Conflict);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPost("transfer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> TransferTokenToTicketOwner([FromBody]TicketTransferModel ticketTransferModel)
        {
            try
            {
                await _tokenTransferService.Transfer(GetLoggedUsername(), ticketTransferModel.TicketId, ticketTransferModel.DestinationAddress);

                return Ok();
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}