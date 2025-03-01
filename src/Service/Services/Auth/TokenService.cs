using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Dtos.Auth;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Interface.Services.Auth;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Service.Services.Auth
{
    public class TokenService(JwtConfigurationDTO jwtSettings) : ITokenService
    {
        private readonly JwtConfigurationDTO _jwtSettings = jwtSettings;
        public Result<string, Error> GenerateToken(UserDTO user)
        {
            try
            {
                Console.WriteLine(_jwtSettings.SecretKey);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.NameIdentifier, user.Id!.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                    ]),
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    SigningCredentials = credentials
                };

                var tokenHandler = new JsonWebTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.CreateToken(token);
            }
            catch (Exception)
            {
                return Error.InternalServerError("Falha ao gerar token! Contate a equipe de desenvolvimento.");
            }
        }

        public Result<bool, Error> ValidateToken(string token)
        {
            try
            {
                // var tokenHandler = new JsonWebTokenHandler();
                // var validationParameters = new TokenValidationParameters
                // {
                //     ValidateIssuerSigningKey = true,
                //     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                //     ValidateIssuer = false,
                //     ValidateAudience = false,
                //     ClockSkew = TimeSpan.Zero
                // };

                // tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}