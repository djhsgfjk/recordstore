using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecordStore.Models
{
    public class PurchaseContext : DbContext
    {
        DbSet<Purchase> Purchases { get; set; }
    }
}