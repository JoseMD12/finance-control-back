
using System.Text;
using Application.Interfaces.Auth;
using Domain.Error;

namespace Application.Service.Auth
{
    public class DecodeBasicAuth : IDecodeBasicAuth
    {
        public (string Email, string Password) Execute(string authHeader)
        {
            var base64 = authHeader["Basic ".Length..].Trim();
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(base64));

            var parts = decoded.Split(':', 2);
            if (parts.Length != 2)
                throw Error.ParseError("Invalid Authorization header format", 400);

            var email = parts[0];
            var password = parts[1];
            return (email, password);
        }
    }
}