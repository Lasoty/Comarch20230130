using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestedProject.Model;

namespace TestedProject
{
    public interface ICalculatorService
    {
        event EventHandler<EventArgs> InvoiceCreated;
        Task<Invoice> CreateInvoice(ICollection<InvoiceItem> items, string contractorName);
        decimal GetGrossFromNet(decimal net, decimal tax);
        DateTime StartPeriodDate(DateTime dateTime);
    }
}