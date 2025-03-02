using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Dtos.User;
using Domain.Entities;

namespace Domain.Interface.Services.User
{
    public interface IUserService
    {
        public Task<Result<Guid, Error>> CreateUser(UserDTO user);
        public Task<Result<List<UserDTO>, Error>> GetAll();
        public Task<Result<UserDTO, Error>> GetByEmail(string email);
        public Task<Result<UserDTO, Error>> UpdateUser(string id, UserDTO user);
    }
}