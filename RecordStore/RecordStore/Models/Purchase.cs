using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecordStore.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Sum { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public ICollection<Record> Records { get; set; }
        public Purchase()
        {
            Records = new List<Record>();
        }
        public DateTime Date { get; set; }
    }

}