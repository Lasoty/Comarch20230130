using NUnit.Framework;
using System;

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
    }
}