using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos.Sheets;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Sheet
{
    [ApiController]
    [Route("/sheet/template")]
    public class SheetTemplateController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> Post([FromBody] SheetTemplateDTO template)
        {
            return Ok();
        }
    }
}