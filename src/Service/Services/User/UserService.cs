
using System.Net;
using Domain.Dtos;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Interface.Services.User;

namespace Service.Services.User
{
    public class UserService(IBaseRepository<UserEntity> userRepository) : IUserService
    {
        private readonly IBaseRepository<UserEntity> _userRepository = userRepository;
        public async Task<Result<Guid, Error>> CreateUser(UserDTO user)
        {
            var users = await _userRepository.GetAll();
            if (!users.IsOk)
            {
                return users.ErrorValue;
            }

            var emailExists = users.Value.Any(x => x.Email == user.Email);
            if (emailExists)
            {
                return new Error(HttpStatusCode.BadRequest, "Email já cadastrado!");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);

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

        public async Task<Result<List<UserDTO>, Error>> GetAll()
        {
            var users = await _userRepository.GetAll();
            if (!users.IsOk)
            {
                return users.ErrorValue;
            }

            return users.Value.Select(x => new UserDTO()
            {
                Id = x.Id.ToString(),
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password
            }).ToList();
        }

        public async Task<Result<UserDTO, Error>> GetByEmail(string email)
        {
            var users = await _userRepository.GetAll();
            if (!users.IsOk)
            {
                return users.ErrorValue;
            }

            var user = users.Value.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new Error(HttpStatusCode.NotFound, "Usuário nao encontrado!");
            }

            return new UserDTO()
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = string.Empty
            };
        }

        public async Task<Result<UserDTO, Error>> UpdateUser(string id, UserDTO user)
        {
            var guidIsValid = Guid.TryParse(id, out Guid userId);

            if (!guidIsValid || userId == Guid.Empty)
            {
                return new Error(HttpStatusCode.BadRequest, "Identificador inválido");
            }

            var userEntity = new UserEntity()
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
            };

            var entity = await _userRepository.Update(userEntity);
            if (!entity.IsOk)
            {
                return entity.ErrorValue;
            }

            return new UserDTO()
            {
                Id = entity.Value.Id.ToString(),
                FirstName = entity.Value.FirstName,
                LastName = entity.Value.LastName,
                Email = entity.Value.Email,
                Password = string.Empty
            };
        }
    }
}