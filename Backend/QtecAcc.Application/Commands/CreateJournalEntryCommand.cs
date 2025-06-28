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
        public List<JournalLineDto> Lines { get; set; }
    }

    public record JournalLineDto
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


            // Prepare DataTable for TVP
            var lineTable = new DataTable();
            lineTable.Columns.Add("AccountId", typeof(int));
            lineTable.Columns.Add("Debit", typeof(decimal));
            lineTable.Columns.Add("Credit", typeof(decimal));

            foreach (var line in request.Lines)
            {
                lineTable.Rows.Add(line.AccountId, line.Debit, line.Credit);
            }

            var lineParam = new SqlParameter("@Lines", SqlDbType.Structured)
            {
                TypeName = "dbo.JournalLineType", // Make sure this matches your TVP name
                Value = lineTable
            };

            command.Parameters.Add(lineParam);
            await command.ExecuteNonQueryAsync(cancellationToken);
            return 1;
        }
    }
}
