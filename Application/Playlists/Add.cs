using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Playlists
{
    public class Add
    {
        public class Command : IRequest
        {
            public string PlaylistName { get; set; }
            public string Name { get; set; }
            public int Duration { get; set; }
            public string Description { get; set; }
            public int Id { get; set; }
            public string Thumbnail { get; set; }
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

                var command = new SqliteCommand("INSERT INTO Videos VALUES (@Name, @Duration, @Description, @DateCreated, @Id, @Thumbnail)", conn);

                command.Parameters.AddWithValue("@Name", request.Name);
                command.Parameters.AddWithValue("@Duration", request.Duration);
                command.Parameters.AddWithValue("@Description", request.Description);
                command.Parameters.AddWithValue("@Id", request.Id);
                command.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                command.Parameters.AddWithValue("@Thumbnail", request.Thumbnail);

                var success = await command.ExecuteNonQueryAsync() > 0;

                var query = new SqliteCommand("SELECT TOP 1 VideoIds FROM Playlists WHERE Name = @PlaylistName", conn);
                query.Parameters.AddWithValue("@PlaylistName", request.PlaylistName);
                var result = await query.ExecuteReaderAsync();
                var videoLists = result.Read().ToString().Split(',').ToList();
                videoLists.Add(request.Name);

                var update = new SqliteCommand("UPDATE Playlists SET VideoIds = @NewVideoIds WHERE Name = @PlaylistName", conn);
                update.Parameters.AddWithValue("@NewVideoIds", videoLists.ToArray().ToString());
                update.Parameters.AddWithValue("@PlaylistName", request.PlaylistName);
                success = await update.ExecuteNonQueryAsync() > 0;

                conn.Close();
                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}