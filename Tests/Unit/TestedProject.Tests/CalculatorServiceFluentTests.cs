using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;

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
            DateTime dateTime = 23.March(2023).At(8,55);

            DateTime actual = calculatorService.StartPeriodDate(dateTime);
            actual.Should().Be(1.March(2023));
        }
    }
}
