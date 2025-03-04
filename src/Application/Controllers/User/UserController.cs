using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Utils;
using Domain.Dtos.User;
using Domain.Interface.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.Tools;

namespace Application.Controllers.User
{
    [ApiController]
    [Route("user")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("create")]
        public async Task<ActionResult> Post([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await _userService.CreateUser(user);
            if (!userId.IsOk)
            {
                return ParseError.Execute(userId.ErrorValue);
            }

            return Ok(userId.Value);
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            if (!users.IsOk)
            {
                return ParseError.Execute(users.ErrorValue);
            }
            return Ok(users.Value);
        }

        [HttpPut("update")]
        [Route("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEntity = await _userService.UpdateUser(id, user);
            if (!userEntity.IsOk)
            {
                return ParseError.Execute(userEntity.ErrorValue);
            }

            return Ok(userEntity.Value);
        }
    }
}
