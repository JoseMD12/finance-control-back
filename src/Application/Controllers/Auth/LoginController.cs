using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Utils;
using Domain.Dtos;
using Domain.Interface.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Auth
{
    [ApiController]
    [Route("/auth/login")]
    public class LoginController(ILoginService loginService) : ControllerBase
    {
        private readonly ILoginService _loginService = loginService;

        [HttpPost]
        public async Task<ActionResult> Login()
        {
            string authHeader = HttpContext.Request.Headers.Authorization!;

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader["Basic ".Length..].Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                int seperatorIndex = usernamePassword.IndexOf(':');

                var email = usernamePassword[..seperatorIndex];
                var password = usernamePassword[(seperatorIndex + 1)..];

                var token = await _loginService.Login(email, password);
                if (!token.IsOk)
                {
                    return ParseError.Execute(token.ErrorValue);
                }

                return Ok(new
                {
                    Token = token.Value
                });
            }
            else
            {
                return BadRequest("Autorização incompleta!");
            }
        }
    }
}