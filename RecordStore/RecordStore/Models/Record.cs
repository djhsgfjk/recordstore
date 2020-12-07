using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordStore.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; }
        public Artist()
        {
            Albums = new List<Album>();
        }

    }

    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public ICollection<Record> Records { get; set; }
        public Album()
        {
            Records = new List<Record>();
        }
    }

    public class Record
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public string Format { get; set; }
        public decimal Price { get; set; }
    }
}