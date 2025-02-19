using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos.User;
using Domain.Entities;

namespace Domain.Interface.Services.User
{
    public interface IUserService
    {
        public Task<Guid> CreateUser(UserDTO user);
        public Task<List<UserEntity>> GetAll();
        // public Task<UserEntity> UpdateUser(string id, UserDTO user);
    }
}