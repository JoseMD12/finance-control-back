using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Dtos.Sheets;

namespace Domain.Interface.Services.Sheet
{
    public interface ISheetTemplateService
    {
        Task<Result<List<SheetTemplateDTO>, Error>> GetAll();
    }
}