using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities.Sheet
{
    public class ColumnTypeValuePairEntity : BaseEntity
    {
        public ColumnType ColumnType { get; set; }
        public string Value { get; set; } = string.Empty;
        public Guid SheetTemplateId { get; set; }
        public SheetTemplateEntity? SheetTemplate { get; set; }
    }
}
