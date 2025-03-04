using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Dtos.User;
using Domain.Entities;

namespace Domain.Interface.Services.Auth
{
    public interface ITokenService
    {
        Result<string, Error> GenerateToken(UserDTO user);
    }
}
