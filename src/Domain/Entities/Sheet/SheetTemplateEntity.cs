using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Sheet
{
    public class SheetTemplateEntity : BaseEntity
    {
        public string TemplateName { get; set; } = string.Empty;
        public string ColorHex { get; set; } = string.Empty;
        public List<ColumnTypeValuePairEntity> Columns { get; set; } = [];
    }
}