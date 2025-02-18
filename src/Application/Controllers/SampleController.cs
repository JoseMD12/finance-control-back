using Domain.Interface.Services.Teste;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("/sample")]
    public class SampleController(ITesteService testeService) : ControllerBase
    {
        private readonly ITesteService _testeService = testeService;
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_testeService.GetHelloWorld());
        }
    }
}