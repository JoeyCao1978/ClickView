using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Playlists
{
    public class Create
    {
        public class Command : IRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Id { get; set; }
            public int VideoIds { get; set; }
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

                var command = new SqliteCommand("INSERT INTO Playlists VALUES (@Name, @Description, @Id, @VideoIds, @DateCreated)", conn);

                command.Parameters.AddWithValue("@Name", request.Name);
                command.Parameters.AddWithValue("@Description", request.Description);
                command.Parameters.AddWithValue("@Id", request.Id);
                command.Parameters.AddWithValue("@VideoIds", request.VideoIds);
                command.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                var success = await command.ExecuteNonQueryAsync() > 0;
                conn.Close();
                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}