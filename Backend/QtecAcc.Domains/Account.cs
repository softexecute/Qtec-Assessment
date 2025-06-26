using System.ComponentModel.DataAnnotations;

namespace QtecAcc.Domains
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;

        public virtual ICollection<JournalLine>? JournalLines { get; set; }
    }

}
