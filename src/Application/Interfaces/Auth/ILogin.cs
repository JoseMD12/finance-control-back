using System.Security.Claims;

namespace Application.Interfaces.Auth
{
    public interface ILogin
    {
        public Task<ClaimsPrincipal> Execute(string authHeader);
    }
}