using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services.User;

namespace Service.Services.User
{
    public class UserService(IBaseRepository<UserEntity> userRepository) : IUserService
    {
        private readonly IBaseRepository<UserEntity> _userRepository = userRepository;
        public async Task<Guid> CreateUser(UserDTO user)
        {
            var users = await _userRepository.GetAll();
            var emailExists = users.Any(x => x.Email == user.Email);
            if (emailExists)
            {
                throw new Exception("Email already exists");
            }

            var userEntityPayload = new UserEntity()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
            };

            var userEntity = await _userRepository.Insert(userEntityPayload);
            return userEntity;
        }

        public async Task<List<UserEntity>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return [.. users.Select(x => new UserEntity()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = string.Empty
            })];
        }

        // public async Task<UserEntity> UpdateUser(string id, UserDTO user)
        // {
        //     var guidIsValid = Guid.TryParse(id, out Guid userId);

        //     if (!guidIsValid)
        //     {
        //         throw new Exception("Id is not valid");
        //     }

        //     var userEntity = new UserEntity()
        //     {
        //         Id = userId,
        //         FirstName = user.FirstName,
        //         LastName = user.LastName,
        //         Email = user.Email,
        //         Password = user.Password,
        //     };

        //     await _userRepository.Update(userEntity);
        // }
    }
}