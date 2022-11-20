using System;
using System.Threading.Tasks;
using Application.Controllers.Base;
using Application.Controllers.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketValidationController : BaseController
    {
        private readonly ILogger<TicketValidationController> _logger;
        private readonly ITicketValidationService _ticketValidationService;

        public TicketValidationController(ILogger<TicketValidationController> logger, ITicketValidationService ticketValidationService)
        {
            _logger = logger;
            _ticketValidationService = ticketValidationService;
        }

        [HttpGet("hash/tickets/{ticketCode}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetTicketHash(string ticketCode)
        {
            try
            {
                string ticketHash = await _ticketValidationService.GetTicketHash(GetLoggedUsername(), ticketCode);

                return Ok(new
                {
                    TicketHash = ticketHash
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting ticket hash");
                return Problem("Error getting ticket hash");
            }
        }

        [HttpPost("ticket/validate")]
        [Authorize(Roles = "Gatekeeper")]
        public async Task<IActionResult> ValidateTicket([FromBody]ValidateTicketModel validationModel)
        {
            try
            {
                bool valid = await _ticketValidationService.IsValid(validationModel.ValidationHash!);

                return Ok(new
                {
                    Valid = valid
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error validating tickets hash");
                return Problem("Error validating tickets hash");
            }
        }
    }
}