﻿using BMICalculator.Model.DTO;
using BMICalculator.Model.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator.Services.Tests
{
    public class ResultServiceTests
    {
        ResultService resultService;
        Mock<IResultRepository> resultRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            resultRepositoryMock = new Mock<IResultRepository>();
            IResultRepository resultRepository = resultRepositoryMock.Object;
            resultService = new ResultService(resultRepository);
        }

        [Test]
        public void SetRecentOverweightResultShouldChangeStateResultService()
        {
            // Arange 
            BmiResult bmi = new()
            {
                Bmi = 30,
                BmiClassification = BmiClassification.Overweight,
                Summary = "You should take care of your obesity"
            };

            // Act
            resultService.SetRecentOverweightResult(bmi);

            // Assert
            Assert.AreEqual(bmi, resultService.RecentOverweightResult);
        }

        [Test]
        public async Task SaveUnderweightResultAsyncShouldCallSaveResultAsyncOnlyOnes()
        {
            // Arrange 
            resultRepositoryMock.Setup(m => m.SaveResultAsync(It.IsAny<BmiResult>())).Verifiable();
            BmiResult bmi = new()
            {
                BmiClassification = BmiClassification.Underweight,
            };

            // Act
            await resultService.SaveUnderweightResultAsync(bmi);

            // Assert
            resultRepositoryMock.Verify(m => m.SaveResultAsync(It.IsAny<BmiResult>()), Times.Once());
        }
    }
}
