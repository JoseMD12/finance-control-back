using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities.Sheet;

namespace Domain.Interface.Repositories
{
    public interface ISheetTemplateRepository : IBaseRepository<SheetTemplateEntity>
    {
        new Task<Result<List<SheetTemplateEntity>, Error>> GetAll();
        new Task<Result<SheetTemplateEntity, Error>> GetById(Guid id);
    }
}
