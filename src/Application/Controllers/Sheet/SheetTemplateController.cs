using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Utils;
using Domain.Dtos.Sheets;
using Domain.Interface.Services.Sheet;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Sheet
{
    [ApiController]
    [Route("sheet-templates")]
    public class SheetTemplateController(ISheetTemplateService service) : ControllerBase
    {
        private readonly ISheetTemplateService _service = service;
        // [HttpPost]
        // public Task<ActionResult> Post([FromBody] SheetTemplateDTO template)
        // {
        //     return Ok();
        // }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var templates = await _service.GetAll();
            if (!templates.IsOk)
            {
                return ParseError.Execute(templates.ErrorValue);
            }

            return Ok(templates.Value);
        }
    }
}
