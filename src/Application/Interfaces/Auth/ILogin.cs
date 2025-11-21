using System.Security.Claims;
using Application.DTO.Auth;

namespace Application.Interfaces.Auth
{
    public interface ILogin
    {
        public Task<ClaimsPrincipal> Execute(string authHeader);
    }
}