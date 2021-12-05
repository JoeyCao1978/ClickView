using System;
using System.Collections.Generic;

namespace Domain
{
    public class PlaylistJson
    {
        public string name { get; set; }
        public string description { get; set; }
        public int id { get; set; }
        public List<string> videoIds { get; set; }
        public DateTime dateCreated { get; set; }
    }
}