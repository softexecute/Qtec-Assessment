

using System.ComponentModel.DataAnnotations;

namespace QtecAcc.Domains
{
    public class JournalLine
    {
        public int Id { get; set; }
        public int JournalId { get; set; }
        public int AccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        public virtual Journal? Journal { get; set; }
        public virtual Account? Account { get; set; }
    }
}
