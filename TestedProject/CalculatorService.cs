using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestedProject.Model;

namespace TestedProject
{
    public class CalculatorService : ICalculatorService
    {
        public event EventHandler<EventArgs> InvoiceCreated;

        public async Task<Invoice> CreateInvoice(ICollection<InvoiceItem> items, string contractorName)
        {
            Invoice invoice = new Invoice()
            {
                Date = DateTime.Now,
                Items = items,
                ReceipientName = contractorName,
            };

            var totalGross = items.Sum(x => GetGrossFromNet(x.NetValue, x.Tax) * x.Quantity);
            var totalNet = items.Sum(x => x.NetValue * x.Quantity);

            invoice.TotalGross = totalGross;
            invoice.TotalNet = totalNet;

            InvoiceCreated?.Invoke(this, EventArgs.Empty);

            return invoice;
        }

        public decimal GetGrossFromNet(decimal net, decimal tax)
        {
            if (tax < 0)
            {
                throw new ArgumentException("Wartość nie może być mniejsza niż 0.", nameof(tax));
            }
            return net * (1 + tax);
        }

        public DateTime StartPeriodDate(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }
    }
}
