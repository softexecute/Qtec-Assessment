using MediatR;
using Microsoft.EntityFrameworkCore;
using QtecAcc.Application.DTOs;
using QtecAcc.Infrastructure;
using System.Data;


namespace QtecAcc.Application.Queries
{
    public class GetAccountsQuery : IRequest<List<AccountDto>>
    {
    }

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountDto>>
    {
        private readonly IDbContext _context;

        public GetAccountsQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();
            command.CommandText = "sp_GetAccounts"; // Name of your stored procedure
            command.CommandType = CommandType.StoredProcedure;

            var accounts = new List<AccountDto>();

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                accounts.Add(new AccountDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Type = reader.GetString(reader.GetOrdinal("Type"))
                });
            }

            return accounts;
        }
    }
}
