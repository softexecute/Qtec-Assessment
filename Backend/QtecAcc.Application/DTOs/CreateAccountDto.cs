using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtecAcc.Application.DTOs
{
    public record CreateAccountDto
    {
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}
