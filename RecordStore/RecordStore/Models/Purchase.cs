using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordStore.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int RecordId { get; set; }
        public DateTime Date { get; set; }
    }
}