using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Playlists
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Name { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                SqliteConnection conn = new SqliteConnection(_context.Database.GetDbConnection().ConnectionString);
                conn.Open();

                var command = new SqliteCommand("DELETE FROM Playlists WHERE Name = @Name", conn);

                command.Parameters.AddWithValue("@Name", request.Name);

                var success = await command.ExecuteNonQueryAsync() > 0;
                conn.Close();
                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}