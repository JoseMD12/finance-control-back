using System.Security.Claims;
using Application.Interfaces.Auth;
using Domain.Entities;
using Domain.Error;
using Infrastructure.Interfaces;

namespace Application.Service.Auth
{
    public class Login(IDecodeBasicAuth decodeBasicAuth, IUser repository) : ILogin
    {
        private readonly IDecodeBasicAuth _decodeBasicAuth = decodeBasicAuth;
        private readonly IUser _repository = repository;

        public async Task<ClaimsPrincipal> Execute(string authHeader)
        {
            (string email, string password) = _decodeBasicAuth.Execute(authHeader);

            User user = await _repository.GetByEmailAsync(email)
                ?? throw Error.ParseError("User not found", 404);

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw Error.ParseError("Invalid password", 401);
            }

            var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                };

            var identity = new ClaimsIdentity(claims, "AppCookie");
            var principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }
}