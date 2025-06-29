using MediatR;
using Microsoft.EntityFrameworkCore;
using QtecAcc.Infrastructure;
using System.Data;

namespace QtecAcc.Application.Commands
{
    public class DeleteAccountCommand : IRequest<int>
    {
        public int Id { get; set; }
     
    }

    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, int>
    {
        private readonly IDbContext _context;

        public DeleteAccountCommandHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();
            command.CommandText = "sp_DeleteAccount";
            command.CommandType = CommandType.StoredProcedure;


            var IdParam = command.CreateParameter();
            IdParam.ParameterName = "@Id";
            IdParam.Value = request.Id;
            command.Parameters.Add(IdParam);

            await command.ExecuteNonQueryAsync(cancellationToken);
            return 1;
        }
    }

}
