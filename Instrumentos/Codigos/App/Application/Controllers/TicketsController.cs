using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Controllers.Base;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketsController : BaseController
    {
        private readonly ILogger<TicketsController> _logger;
        private readonly ITicketService _userService;

        public TicketsController(ILogger<TicketsController> logger, ITicketService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("owned")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> MyTickets()
        {
            try
            {
                var tickets = await _userService.ListMyTickets(GetLoggedUsername());

                return Ok(tickets.Select(ticket => new
                {
                    Code = ticket.Ticket.Code,
                    Used = ticket.Ticket.UsedOnEvent,
                    Ticket = new
                    {
                        StartDate = ticket.EventTicketType.StartDate,
                        EndDate = ticket.EventTicketType.EndDate,
                        Name = ticket.EventTicketType.TicketName,
                        Qualification = ticket.EventTicketType.Qualification,
                        PriceBrl = ticket.EventTicketType.PriceBrl
                    },
                    Event = new
                    {
                        Title = ticket.Event.Title,
                        ImageUrl = ticket.Event.ImageUrl
                    }
                }));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error listing users tickets");
                return Problem("Error listing users tickets");
            }
        }
    }
}