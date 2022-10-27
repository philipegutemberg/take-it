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

        [HttpGet("ticket")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetTicketImage([FromBody]GetTicketImageModel getTicketImageModel)
        {
            try
            {
                byte[] fileByteArray = await _ticketValidationService.GetTicketImage(getTicketImageModel.TicketId!);

                return File(fileByteArray, "image/png");
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}