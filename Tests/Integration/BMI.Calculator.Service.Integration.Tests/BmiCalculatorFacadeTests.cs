using BMICalculator.Model.Context;
using BMICalculator.Model.DTO;
using BMICalculator.Model.Repositories;
using BMICalculator.Services;
using BMICalculator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace BMI.Calculator.Service.Integration.Tests
{
    public class BmiCalculatorFacadeTests
    {
        IResultRepository resultRepository;
        IBmiCalculatorFacade bmiCalculatorFacade;
        AppDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            DbContextOptions options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BmiDb")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            dbContext = new AppDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            resultRepository = new ResultRepository(dbContext);

            var bmiDeterminatorMock = new Mock<IBmiDeterminator>();
            var bmiCalculatorFactoryMock = new Mock<IBmiCalculatorFactory>();

            bmiCalculatorFacade =
                new BmiCalculatorFacade(bmiDeterminatorMock.Object, bmiCalculatorFactoryMock.Object, resultRepository);
        }

        [Test]
        public async Task SaveResultShouldSaveBmiRecordInDb()
        {
            // Arrange
            BmiResult bmi = new BmiResult()
            {
                Id = 1,
                Bmi = 15,
                BmiClassification = BmiClassification.Normal,
                Summary = "Jest OK"
            };

            // Act
            await bmiCalculatorFacade.SaveResult(bmi);

            // Assert
            Assert.IsTrue(dbContext.Results.Any(x => x.Id == bmi.Id && x.BmiClassification == BmiClassification.Normal));
        }
    }
}