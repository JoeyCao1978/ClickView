using System;

namespace Domain
{
    public class Playlist
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string VideoIds { get; set; }
        public DateTime DateCreated { get; set; }
    }
}