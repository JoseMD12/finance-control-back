using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Interface.Services.Auth;
using Domain.Interface.Services.User;
using Utils.Tools;

namespace Service.Services.Auth
{
    public class LoginService(IUserService userService, ITokenService tokenService) : ILoginService
    {
        private readonly IUserService _userService = userService;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<Result<string, Error>> Login(string email, string password)
        {
            var users = await _userService.GetAll();
            if (!users.IsOk)
            {
                return users.ErrorValue;
            }

            var user = users.Value.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return Error.NotFound("Usuário não encontrado!");
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isValidPassword)
            {
                return Error.Unauthorized("Senha inválida!");
            }

            var token = _tokenService.GenerateToken(user);

            if (!token.IsOk)
            {
                return token.ErrorValue;
            }

            return token.Value;
        }
    }
}
