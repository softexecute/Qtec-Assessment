using QtecAcc.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtecAcc.Application.DTOs
{
    public record JournalDetailDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public decimal TotalAmount { get; set; }

        public List<JournalLinesDto> Lines { get; set; }
    }
}
