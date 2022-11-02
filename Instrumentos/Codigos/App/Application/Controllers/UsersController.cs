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

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly ICustomerService _customerUserService;
        private readonly ISpecialUserService _specialUserService;
        private readonly HashService _hashService;

        public UsersController(
            ICustomerService customerUserService,
            ISpecialUserService specialUserService,
            HashService hashService)
        {
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
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPost("special/register")]
        [Authorize(Roles = "Admin")]
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

                    case EnumUserRole.Admin:
                    case EnumUserRole.Customer:
                    default:
                        throw new ArgumentOutOfRangeException(newUser.Role.ToString());
                }

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return Problem();
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
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}