using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtecAcc.Application.DTOs
{
    public record CreateJournalEntryDto
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = default!;
        public List<JournalEntryLineDto> Lines { get; set; } = new();
    }
}
