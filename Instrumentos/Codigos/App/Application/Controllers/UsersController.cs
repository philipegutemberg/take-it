using System;
using System.Net;
using System.Threading.Tasks;
using Application.Controllers.Base;
using Application.Controllers.Models;
using Application.Services;
using Domain.Enums;
using Domain.Models.Users;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ICustomerService _customerUserService;
        private readonly ISpecialUserService _specialUserService;
        private readonly HashService _hashService;

        public UsersController(
            ILogger<UsersController> logger,
            ICustomerService customerUserService,
            ISpecialUserService specialUserService,
            HashService hashService)
        {
            _logger = logger;
            _customerUserService = customerUserService;
            _specialUserService = specialUserService;
            _hashService = hashService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(NewUserModel newUser)
        {
            try
            {
                string password = _hashService.GetHash(newUser.Password!);

                await _customerUserService.Create(new CustomerUser(
                    newUser.Username!,
                    password,
                    newUser.FullName!,
                    newUser.Email!,
                    newUser.Phone!,
                    newUser.WalletAddress!
                ));

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error registering consumer");
                return Problem("Error registering consumer");
            }
        }

        [HttpPost("special/register")]
        [Authorize(Roles = "Backoffice")]
        public async Task<IActionResult> RegisterSpecialUser(NewSpecialUserModel newUser)
        {
            try
            {
                string password = _hashService.GetHash(newUser.Password!);

                switch (newUser.Role)
                {
                    case EnumUserRole.Backoffice:
                    {
                        await _specialUserService.CreateBackOfficeUser(new BackOfficeUser(
                            newUser.Username!,
                            password
                        ));
                        break;
                    }

                    case EnumUserRole.Gatekeeper:
                    {
                        await _specialUserService.CreateGatekeeperUser(new GatekeeperUser(
                            newUser.Username!,
                            password
                        ));
                        break;
                    }

                    case EnumUserRole.Customer:
                    default:
                        throw new ArgumentOutOfRangeException(newUser.Role.ToString());
                }

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error registering special user");
                return Problem("Error registering special user");
            }
        }

        [HttpGet("customer/address")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAddress()
        {
            try
            {
                var address = await _customerUserService.GetInternalAddress(GetLoggedUsername());

                return Ok(new
                {
                    Address = address
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting customers wallet address");
                return Problem("Error getting customers wallet address");
            }
        }
    }
}