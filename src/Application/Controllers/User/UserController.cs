using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos.User;
using Domain.Interface.Services.User;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(userId);
        }

        [HttpGet("list")]
        public async Task<ActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        // [HttpPut("update")]
        // [Route("{id}")]
        // public async Task<ActionResult> Update(string id, [FromBody] UserDTO user)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     await _userService.UpdateUser(id, user);
        //     return Ok();
        // }
    }
}