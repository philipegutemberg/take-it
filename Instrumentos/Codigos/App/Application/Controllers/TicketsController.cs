using System;
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

                return Ok(tickets);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error listing users tickets");
                return Problem("Error listing users tickets");
            }
        }
    }
}