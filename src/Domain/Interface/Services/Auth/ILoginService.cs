using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;

namespace Domain.Interface.Services.Auth
{
    public interface ILoginService
    {
        Task<Result<string, Error>> Login(string email, string password);
    }
}
