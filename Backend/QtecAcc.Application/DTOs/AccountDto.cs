
namespace QtecAcc.Application.DTOs
{
    public record AccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}
