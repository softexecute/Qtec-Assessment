using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QtecAcc.Application.DTOs;
using QtecAcc.Infrastructure;
using System.Data;



namespace QtecAcc.Application.Queries
{
    public class GetJournalDetailsQuery : IRequest<JournalDetailDto>
    {
        public int Id { get; set; }
    }
    public class GetJournalDetailsQueryHandler : IRequestHandler<GetJournalDetailsQuery, JournalDetailDto>
    {
        private readonly IDbContext _context;

        public GetJournalDetailsQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<JournalDetailDto> Handle(GetJournalDetailsQuery request, CancellationToken cancellationToken)
        {

            JournalDetailDto journalDict = null;
            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();
            command.CommandText = "sp_GetJournalsDetails";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Id", request.Id));

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                if (journalDict == null)
                {
                    journalDict = new JournalDetailDto();
                    journalDict.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    journalDict.Date = reader.GetDateTime(reader.GetOrdinal("Date"));
                    journalDict.Description = reader.GetString(reader.GetOrdinal("Description"));
                    journalDict.TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"));
                    journalDict.Lines = new List<JournalLinesDto>();
                }


                // Add Line
                if (!reader.IsDBNull(reader.GetOrdinal("AccountName")))
                {
                    journalDict.Lines.Add(new JournalLinesDto
                    {
                        AccountName = reader.GetString(reader.GetOrdinal("AccountName")),
                        Debit = reader.GetDecimal(reader.GetOrdinal("Debit")),
                        Credit = reader.GetDecimal(reader.GetOrdinal("Credit"))
                    });
                }

            }

            return journalDict;
        }

    }



    }
