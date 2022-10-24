using System;
using System.Threading.Tasks;
using Application.Controllers.Base;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketsController : BaseController
    {
        private readonly ITicketService _userService;

        public TicketsController(ITicketService userService)
        {
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
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}