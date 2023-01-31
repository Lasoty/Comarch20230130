using BMICalculator.Model.DTO;
using BMICalculator.Services.Enums;
using BMICalculator.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator.Services.Tests
{
    public class BmiCalculatorFacadeTests
    {
        Mock<IBmiCalculatorFactory> bmiCalculatorFactoryMock;
        Mock<IBmiDeterminator> bmiDeterminatorMock;

        IBmiCalculatorFacade bmiCalculatorFacade;

        [SetUp]
        public void SetUp()
        {
            bmiDeterminatorMock= new Mock<IBmiDeterminator>();
            bmiCalculatorFactoryMock = new Mock<IBmiCalculatorFactory>();

            bmiCalculatorFacade = new BmiCalculatorFacade(bmiDeterminatorMock.Object, bmiCalculatorFactoryMock.Object);
        }


        [Test]
        public void GetResultShouldResultUnderweightWhenWeightIsToLow() 
        {
            // Arrange
            bmiCalculatorFactoryMock.Setup(m => m.CreateCalculator(It.IsAny<UnitSystem>()))
                .Returns(new MetricBmiCalculator());

            // Act
            var actual = bmiCalculatorFacade.GetResult(50, 200, UnitSystem.Metric);

            // Assert
            Assert.NotNull(actual);
            Assert.AreEqual(BmiClassification.Underweight, actual.BmiClassification);
        }
    }
}
