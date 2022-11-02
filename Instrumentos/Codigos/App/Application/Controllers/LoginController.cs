using System;
using System.Threading.Tasks;
using Application.Controllers.Base;
using Application.Controllers.Models;
using Application.Services;
using Domain.Exceptions;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly TokenService _tokenService;
        private readonly LoginService _loginService;

        public LoginController(TokenService tokenService, LoginService loginService)
        {
            _tokenService = tokenService;
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]LoginModel model)
        {
            try
            {
                GenericUser? loggedUser = await _loginService.TryLogin(model.Username!, model.Password!);
                if (loggedUser != null)
                {
                    // Gera o Token
                    var token = _tokenService.GenerateToken(loggedUser);

                    // Retorna os dados
                    return Ok(new
                    {
                        loggedUser.Username,
                        token = token
                    });
                }

                return Unauthorized();
            }
            catch (RepositoryException)
            {
                return NotFound(new { message = "Invalid user" });
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}