using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecordStore.Models
{
    public class RecordContext : DbContext
    {
        public RecordContext() : base("RecordContext")
        { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Alubms { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}