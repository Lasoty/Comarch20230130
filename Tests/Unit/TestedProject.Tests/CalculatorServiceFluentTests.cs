using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using System.ComponentModel.DataAnnotations;
using TestedProject.Model;

namespace TestedProject.Tests
{
    public class CalculatorServiceFluentTests
    {
        ICalculatorService calculatorService;

        [SetUp]
        public void SetUp()
        {
            calculatorService = new CalculatorService();
        }

        [Test]
        public void StartPeriodDateShouldReturnValidStartDate()
        {
            DateTime dateTime = 23.March(2023).At(8, 55);

            DateTime actual = calculatorService.StartPeriodDate(dateTime);
            actual.Should().Be(1.March(2023));
        }

        [Test]
        public void CreateInvoiceShouldReturnInvoiceWithExactItems()
        {
            ICollection<InvoiceItem> items = new List<InvoiceItem>()
            {
                new InvoiceItem { Name = "Item test 1", NetValue = 10, Tax = 0,  Quantity = 1 },
                new InvoiceItem { Name = "Item test 2", NetValue = 10, Tax = 0.8m,  Quantity = 2 },
                new InvoiceItem { Name = "Item test 3", NetValue = 10, Tax = 0.23m, Quantity = 3 },
            };

            string contractorName = "Test contractor";

            Invoice actual = calculatorService.CreateInvoice(items, contractorName);

            actual.Should().NotBeNull();
            actual.ReceipientName.Should().Be(contractorName);
            actual.TotalGross.Should().Be(82.9m);
            actual.TotalNet.Should().Be(60);

            actual.Items.Should().NotBeEmpty().And.HaveCount(3).And.ContainItemsAssignableTo<InvoiceItem>()
                .And.Equal(items);
        }
    }
}
