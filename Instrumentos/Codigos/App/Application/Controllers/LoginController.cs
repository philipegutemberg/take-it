using System;
using System.Threading.Tasks;
using Application.Controllers.Models;
using Application.Services;
using Domain;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : Controller
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
                User user = await _loginService.Login(model.Username, model.Password);

                // Gera o Token
                var token = _tokenService.GenerateToken(user);

                // Retorna os dados
                return Ok(new
                {
                    user.Username,
                    user.Role,
                    token = token
                });
            }
            catch (UserNotFoundException e)
            {
                return NotFound(new { message = "Invalid user" });
            }
            catch (Exception e)
            {
                return Problem();
            }
        }
    }
}