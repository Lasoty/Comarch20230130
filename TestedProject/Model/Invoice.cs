using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestedProject.Model
{
    public class Invoice
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalNet { get; set; }

        public decimal TotalGross { get; set; }

        public string ReceipientName { get; set; }

        public ICollection<InvoiceItem> Items { get; set; }
    }

    public class InvoiceItem
    {
        public string Name { get; set; }

        public decimal NetValue { get; set; }

        public decimal Tax { get; set; }

        public decimal Quantity { get; set; }
    }
}