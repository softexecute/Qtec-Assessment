using MediatR;
using Microsoft.EntityFrameworkCore;
using QtecAcc.Infrastructure;
using System.Data;

namespace QtecAcc.Application.Commands
{
    public class CreateAccountCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
    }

    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IDbContext _context;

        public CreateAccountHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();
            command.CommandText = "sp_CreateAccount";
            command.CommandType = CommandType.StoredProcedure;


            var nameParam = command.CreateParameter();
            nameParam.ParameterName = "@Name";
            nameParam.Value = request.Name;
            command.Parameters.Add(nameParam);

            var typeParam = command.CreateParameter();
            typeParam.ParameterName = "@Type";
            typeParam.Value = request.Type;
            command.Parameters.Add(typeParam);

            var idParam = command.CreateParameter();
            idParam.ParameterName = "@NewId";
            idParam.Direction = ParameterDirection.Output;
            idParam.DbType = DbType.Int32;
            command.Parameters.Add(idParam);

            await command.ExecuteNonQueryAsync(cancellationToken);

            return (int)(idParam.Value ?? 0);
        }
    }

}
