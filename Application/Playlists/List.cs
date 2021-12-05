using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Playlists
{
    public class List
    {
        public class Query : IRequest<List<Playlist>> { }

        public class Handler : IRequestHandler<Query, List<Playlist>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Playlist>> Handle(Query request, CancellationToken cancellationToken)
            {
                SqliteConnection conn = new SqliteConnection(_context.Database.GetDbConnection().ConnectionString);
                conn.Open();

                var command = new SqliteCommand("SELECT * FROM Playlists", conn);

                var result = await command.ExecuteReaderAsync();

                var playlists = new List<Playlist>();

                while (result.Read())
                {
                    playlists.Add(new Playlist
                    {
                        Name = result["Name"].ToString(),
                        Description = result["Description"].ToString(),
                        Id = Int32.Parse(result["Id"].ToString()),
                        VideoIds = result["VideoIds"].ToString(),
                        DateCreated = DateTime.Parse(result["DateCreated"].ToString()),
                    });
                }

                conn.Close();

                return playlists;
            }
        }
    }
}