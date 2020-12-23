using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecordStore.Models
{
    public class RecordContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>().HasMany(c => c.Records)
                .WithMany(s => s.Purchases)
                .Map(t => t.MapLeftKey("PurchaseId")
                .MapRightKey("RecordId")
                .ToTable("PurchaseRecords"));
        }
        public DbSet<PurchaseRecord> PurchaseRecords { get; set; }

                public RecordContext() : base("RecordContext")
        { }

    }
}