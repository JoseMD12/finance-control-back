
using System.Security.Claims;
using Api.ErrorHandler;
using Application.Interfaces.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Api.Controllers.Auth
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class Login(ILogin login) : ControllerBase
    {
        private readonly ILogin _login = login;

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                if (!Request.Headers.TryGetValue("Authorization", out StringValues value))
                    return Unauthorized();

                var authHeader = value.ToString();
                if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                    return Unauthorized();

                ClaimsPrincipal principal = await _login.Execute(authHeader);
                Console.WriteLine("User authenticated: " + principal.Identity?.Name);
                await HttpContext.SignInAsync("AppCookie", principal);

                return Ok(new { Message = "Login successful", User = principal.Identity?.Name });
            }
            catch (Exception ex)
            {
                var errorResponse = Error.Handler(ex);
                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
        }
    }
}