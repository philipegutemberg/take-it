using System;
using System.Threading.Tasks;
using Application.Controllers.Base;
using Application.Controllers.Models;
using Domain.Exceptions;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketValidationController : BaseController
    {
        private readonly ITicketValidationService _ticketValidationService;

        public TicketValidationController(ITicketValidationService ticketValidationService)
        {
            _ticketValidationService = ticketValidationService;
        }

        [HttpGet("image/{ticketCode}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetTicketImage(string ticketCode)
        {
            try
            {
                string ticketHash = await _ticketValidationService.GetTicketHash(GetLoggedUsername(), ticketCode);

                return Ok(new
                {
                    TicketHash = ticketHash
                });
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPost("ticket")]
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
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}