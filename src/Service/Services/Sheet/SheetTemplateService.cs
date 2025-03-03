using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Dtos.Sheets;
using Domain.Entities.Sheet;
using Domain.Interface.Repositories;

namespace Service.Services.Sheet
{
    public class TemplateSheetService(IBaseRepository<SheetTemplateEntity> repository)
    {
        private readonly IBaseRepository<SheetTemplateEntity> _repository = repository;

        public async Task<Result<List<SheetTemplateDTO>, Error>> GetAll()
        {
            var templates = await _repository.GetAll();
            // return templates.Value.Select(x => new SheetTemplateDTO()
            // {
            //     Id = x.Id.ToString(),
            //     Name = x.Name,
            //     Description = x.Description
            // }).ToList();

            return new();
        }


    }
}