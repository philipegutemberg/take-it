using System;
using System.Threading.Tasks;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("tickets")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> MyTickets()
        {
            try
            {
                var tickets = await _userService.ListMyTickets(User.Identity.Name);

                return Ok(tickets);
            }
            catch (Exception e)
            {
                return Problem();
            }
        }
    }
}