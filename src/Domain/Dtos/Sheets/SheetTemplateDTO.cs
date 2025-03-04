using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dtos.Sheets
{
    public class SheetTemplateDTO
    {
        public string? Id { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public string ColorHex { get; set; } = string.Empty;
        public List<ColumnTypeValuePairDTO> Columns { get; set; } = [];
    }
}
