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
    public class GetTrialBalanceQuery : IRequest<List<TrialBalanceDTo>>
    {

    }

    internal class GetTrialBalanceQueryHandler : IRequestHandler<GetTrialBalanceQuery, List<TrialBalanceDTo>>
    {
        private readonly IDbContext _context;

        public GetTrialBalanceQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrialBalanceDTo>> Handle(GetTrialBalanceQuery request, CancellationToken cancellationToken)
        {
            var result = new List<TrialBalanceDTo>();

            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();
            command.CommandText = "sp_GetTrialBalance";
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                result.Add(new TrialBalanceDTo
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Account = reader.GetString(reader.GetOrdinal("Account")),
                    Type = reader.GetString(reader.GetOrdinal("Type")),
                    Debit = reader.GetDecimal(reader.GetOrdinal("Debit")),
                    Credit = reader.GetDecimal(reader.GetOrdinal("Credit")),
                    Balance = reader.GetDecimal(reader.GetOrdinal("Balance"))
                });
            }

            return result;
        }
    }

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
