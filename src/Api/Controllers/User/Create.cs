using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.User;

namespace Api.Controllers.User
{
    [ApiController]
    [Route("api/user/[controller]")]
    public class Create(IUser service) : ControllerBase
    {
        private readonly IUser _service = service;
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var user = await _service.CreateAsync(new Domain.Entities.User());
            return Ok(new { Message = "User created", User = user });
        }
    }
}