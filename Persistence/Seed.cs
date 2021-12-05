using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;
using Newtonsoft.Json;

namespace Persistence
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            using (StreamReader r = new StreamReader(currentDirectory+"\\data\\videos.json"))
            {
                string json = r.ReadToEnd();
                List<Video> videos = JsonConvert.DeserializeObject<List<Video>>(json);
                context.Videos.AddRange(videos);
                context.SaveChanges();
            }

            using (StreamReader r = new StreamReader(currentDirectory + "\\data\\playlists.json"))
            {
                string json = r.ReadToEnd();
                List<PlaylistJson> playlistjsons = JsonConvert.DeserializeObject<List<PlaylistJson>>(json);
                var playlists = new List<Playlist>();
                for(int i = 0; i < playlistjsons.Count; i++)
                {
                    playlists.Add(new Playlist
                    {
                        Name = playlistjsons[i].name,
                        Description = playlistjsons[i].description,
                        Id = playlistjsons[i].id,
                        VideoIds = string.Join(",", playlistjsons[i].videoIds),
                        DateCreated = playlistjsons[i].dateCreated,

                    });
                };
                context.Playlists.AddRange(playlists);
                context.SaveChanges();
            }
        }
    }
}