using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Dtos.Sheets;
using Domain.Entities.Sheet;
using Domain.Interface.Repositories;
using Domain.Interface.Services.Sheet;

namespace Service.Services.Sheet
{
    public class SheetTemplateService(ISheetTemplateRepository repository) : ISheetTemplateService
    {
        private readonly ISheetTemplateRepository _repository = repository;

        public async Task<Result<List<SheetTemplateDTO>, Error>> GetAll()
        {
            var templates = await _repository.GetAll();

            if (!templates.IsOk)
            {
                return templates.ErrorValue;
            }

            return templates.Value.Select(x => new SheetTemplateDTO()
            {
                Id = x.Id.ToString(),
                ColorHex = x.ColorHex,
                TemplateName = x.TemplateName,
                Columns = [.. x.Columns.Select(y => new ColumnTypeValuePairDTO()
                {
                    Id = y.Id.ToString(),
                    ColumnType = y.ColumnType,
                    Value = y.Value
                })]
            }).ToList();
        }
    }
}
