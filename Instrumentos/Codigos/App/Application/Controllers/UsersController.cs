using System;
using System.Threading.Tasks;
using Application.Controllers.Base;
using Application.Controllers.Models;
using Application.Services;
using Domain.Models.Users;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly HashService _hashService;

        public UsersController(IUserService userService, HashService hashService)
        {
            _userService = userService;
            _hashService = hashService;
        }

        [HttpGet("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(NewUserModel newUser)
        {
            try
            {
                string password = _hashService.GetHash(newUser.Password!);

                await _userService.Create(new Customer(
                    newUser.Username!,
                    password,
                    newUser.FullName!,
                    newUser.Email!,
                    newUser.Phone!,
                    newUser.WalletAddress!
                ));

                return Ok();
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}