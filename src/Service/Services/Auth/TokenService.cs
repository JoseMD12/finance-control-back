using System.Security.Claims;
using System.Text;
using Domain.Dtos;
using Domain.Dtos.Auth;
using Domain.Dtos.User;
using Domain.Interface.Services.Auth;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Utils.Tools;

namespace Service.Services.Auth
{
    public class TokenService(JwtConfigurationDTO jwtSettings) : ITokenService
    {
        private readonly JwtConfigurationDTO _jwtSettings = jwtSettings;

        public Result<string, Error> GenerateToken(UserDTO user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)
                );
                var credentials = new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256
                );

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        [new Claim("id", user.Id!.ToString()), new Claim("email", user.Email)]
                    ),
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    SigningCredentials = credentials,
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                };

                var tokenHandler = new JsonWebTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return token;
            }
            catch (Exception)
            {
                return Error.InternalServerError(
                    "Falha ao gerar token! Contate a equipe de desenvolvimento."
                );
            }
        }
    }
}
