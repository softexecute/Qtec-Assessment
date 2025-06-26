using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QtecAcc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QtecAcc.Application.Commands
{
    public class CreateJournalCommand : IRequest<int>
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public List<JournalLineDto> Lines { get; set; } = new();
    }

    public class JournalLineDto
    {
        public int AccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }

    public class CreateJournalHandler : IRequestHandler<CreateJournalCommand, int>
    {
        private readonly IDbContext _context;

        public CreateJournalHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateJournalCommand request, CancellationToken cancellationToken)
        {
            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();
            command.CommandText = "sp_CreateJournalEntry";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Date", request.Date));
            command.Parameters.Add(new SqlParameter("@Description", request.Description));

            // You may need to serialize JournalLine list as JSON or pass via TVP depending on how your SP is written

            var journalIdParam = new SqlParameter("@NewJournalId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(journalIdParam);

            await command.ExecuteNonQueryAsync(cancellationToken);
            return (int)(journalIdParam.Value ?? 0);
        }
    }
}
