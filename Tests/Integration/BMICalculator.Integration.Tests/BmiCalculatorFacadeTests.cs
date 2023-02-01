using BMICalculator.Model.DTO;
using BMICalculator.Model.Repositories;
using BMICalculator.Services;
using BMICalculator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using NUnit.Framework;
using BMICalculator.Model.Data;
using BMICalculator.Model.Model;
using Autofac;

namespace BMI.Calculator.Service.Integration.Tests
{
    public class BmiCalculatorFacadeTests
    {
        IBmiCalculatorFacade bmiCalculatorFacade;
        ApplicationDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            IContainer container = PrepareContainer();

            dbContext = container.Resolve<ApplicationDbContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            bmiCalculatorFacade = container.Resolve<IBmiCalculatorFacade>();
        }

        private IContainer PrepareContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BmiDb")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            builder.RegisterInstance(options);
            builder.RegisterType<ApplicationDbContext>();
            builder.RegisterType<ResultRepository>().AsImplementedInterfaces();

            var bmiDeterminatorMock = new Mock<IBmiDeterminator>();
            var bmiCalculatorFactoryMock = new Mock<IBmiCalculatorFactory>();
            builder.RegisterInstance(bmiDeterminatorMock.Object);
            builder.RegisterInstance(bmiCalculatorFactoryMock.Object);
            builder.RegisterType<BmiCalculatorFacade>().As<IBmiCalculatorFacade>();


            return builder.Build();
        }

        [Test]
        public async Task SaveResultShouldSaveBmiRecordInDb()
        {
            Guid id = Guid.NewGuid();
            // Arrange
            BmiMeasurement bmi = new BmiMeasurement()
            {
                Id = id,
                Bmi = 15,
                BmiClassification = BmiClassification.Normal,
                Summary = "Jest OK"
            };

            // Act
            await bmiCalculatorFacade.SaveResult(bmi);

            // Assert
            Assert.IsTrue(dbContext.BmiMeasurements.Any(x => x.Id == bmi.Id && x.BmiClassification == BmiClassification.Normal));
        }
    }
}