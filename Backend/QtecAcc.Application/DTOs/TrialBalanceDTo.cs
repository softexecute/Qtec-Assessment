namespace QtecAcc.Application.DTOs
{
    public record TrialBalanceDTo
    {
        public int Id { get; set; }
        public string? Account { get; set; }
        public string? Type { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}
