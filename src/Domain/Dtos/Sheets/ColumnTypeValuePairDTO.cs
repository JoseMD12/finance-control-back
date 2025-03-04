using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Dtos.Sheets
{
    public class ColumnTypeValuePairDTO
    {
        public string? Id { get; set; }
        public ColumnType ColumnType { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
