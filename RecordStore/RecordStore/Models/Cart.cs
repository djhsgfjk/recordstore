using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Record record, int quantity)
        {
            CartLine line = lineCollection
                .Where(r => r.Record.Id == record.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Record = record,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public CartLine GetLine(Record record)
        {
            return lineCollection.First();
        }
        public void RemoveLine(Record record)
        {
            lineCollection.RemoveAll(l => l.Record.Id == record.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Record.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    
        public class CartLine
        {
            public Record Record { get; set; }
            public int Quantity { get; set; }
        }

        public class CartIndexViewModel
        {
            public Cart Cart { get; set; }
        }
    }
}