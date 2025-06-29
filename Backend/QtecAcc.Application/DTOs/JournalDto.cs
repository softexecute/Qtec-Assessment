
namespace QtecAcc.Application.DTOs
{
    public class JournalDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
