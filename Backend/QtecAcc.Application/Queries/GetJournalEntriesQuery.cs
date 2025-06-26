using MediatR;
using Microsoft.EntityFrameworkCore;
using QtecAcc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtecAcc.Application.Queries
{
    public class GetJournalEntriesQuery : IRequest<List<JournalDto>> { }
    public class GetJournalEntriesHandler : IRequestHandler<GetJournalEntriesQuery, List<JournalDto>>
    {
        private readonly IDbContext _context;

        public GetJournalEntriesHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<JournalDto>> Handle(GetJournalEntriesQuery request, CancellationToken cancellationToken)
        {
            var journals = new List<JournalDto>();

            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();
            command.CommandText = "sp_GetJournalEntries";
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                journals.Add(new JournalDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    TotalDebit = reader.GetDecimal(reader.GetOrdinal("TotalDebit")),
                    TotalCredit = reader.GetDecimal(reader.GetOrdinal("TotalCredit"))
                });
            }

            return journals;
        }
    }
    public class JournalDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
    }
}
