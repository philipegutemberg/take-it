using System;
using System.Threading.Tasks;
using Application.Controllers.Base;
using Application.Controllers.Models;
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
                byte[] fileByteArray = await _ticketValidationService.GetTicketImage(GetLoggedUsername(), ticketCode);

                return File(fileByteArray, "image/png");
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPost("ticket")]
        [Authorize(Roles = "Gatekeeper")]
        public async Task<IActionResult> ValidateTicket([FromBody]string validationText)
        {
            try
            {
                bool valid = await _ticketValidationService.IsValid(validationText);

                if (valid)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}