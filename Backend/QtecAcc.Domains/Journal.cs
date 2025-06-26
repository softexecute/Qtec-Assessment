using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtecAcc.Domains
{
    public class Journal
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = default!;
        public virtual ICollection<JournalLine>? Lines { get; set; }
    }
}
