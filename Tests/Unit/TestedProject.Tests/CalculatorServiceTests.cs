using TestedProject.Model;

namespace TestedProject.Tests
{
    public class CalculatorServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(10, 0.23, 12.3)]
        [TestCase(20, 0.23, 24.6)]
        public void GetGrossFromNetShouldReturnValidValue(decimal netValue, decimal tax, decimal expected)
        {
            // Arrange
            ICalculatorService calculatorService = new CalculatorService();

            // Act
            decimal actual = calculatorService.GetGrossFromNet(netValue, tax);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetGrossFromNetShouldThrowsExceptionWhenTaxLess0()
        {
            // Arrange
            ICalculatorService calculatorService = new CalculatorService();
            decimal net = 10;
            decimal tax = -1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => calculatorService.GetGrossFromNet(net, tax));
        }

        [Test]
        public async Task CreateInvoiceShouldReturnExactInvoiceItems()
        {
            //Arrange
            ICalculatorService calculatorService = new CalculatorService();
            ICollection<InvoiceItem> items = new List<InvoiceItem>()
            {
                new InvoiceItem { Name = "Item test 1", NetValue = 10, Tax = 0,  Quantity = 1 },
                new InvoiceItem { Name = "Item test 2", NetValue = 10, Tax = 0.8m,  Quantity = 2 },
                new InvoiceItem { Name = "Item test 3", NetValue = 10, Tax = 0.23m, Quantity = 3 },
            };
            string contractorName = "Test contractor";

            decimal totalGrossExpected = 82.9m;


            //Act 
            var actual = await calculatorService.CreateInvoice(items, contractorName);

            //Assert
            Assert.AreEqual(totalGrossExpected, actual.TotalGross);
            CollectionAssert.AllItemsAreNotNull(actual.Items);
            CollectionAssert.AllItemsAreInstancesOfType(actual.Items, typeof(InvoiceItem));
            CollectionAssert.AreEqual(items, actual.Items);
        }
    }
}