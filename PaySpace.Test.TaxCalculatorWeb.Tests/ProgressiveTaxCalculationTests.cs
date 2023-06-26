using Microsoft.EntityFrameworkCore;
using PaySpace.Test.TaxCalculatorWeb.Data;
using PaySpace.Test.TaxCalculatorWeb.Services;

namespace PaySpace.Test.TaxCalculatorWeb.Tests
{
    public class ProgressiveTaxCalculationTests
    {
        private TaxDbContext _dbContext;
        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<TaxDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            _dbContext = new TaxDbContext(options);

            var created = await _dbContext.Database.EnsureCreatedAsync();
            if (created)
            {
                var seedData = new SeedData(null, _dbContext);
                await seedData.SeedAsync();
            }
        }

        [Test]
        [TestCase(6351, 635.1)]
        [TestCase(8350, 835)]
        [TestCase(8351, 835.15)]
        [TestCase(30951, 4225)]
        [TestCase(33950, 4674.85)]
        [TestCase(33951, 4675.1)]
        [TestCase(80251, 16249.85)]
        [TestCase(82250, 16749.6)]
        [TestCase(82251, 16749.88)]
        [TestCase(170551, 41473.6)]
        [TestCase(171550, 41753.32)]
        [TestCase(171551, 41753.65)]
        [TestCase(350951, 100955.32)]
        [TestCase(372950, 108214.99)]
        [TestCase(372951, 108215.34)]
        [TestCase(400000, 117682.14)]
        public async Task TaxShouldBeCalculatedCorrectly (decimal income, decimal expectedTax)
        {
            var taxCalculator = new ProgressiveTaxCalculator(_dbContext);

            var actualTax = await taxCalculator.CalculateAsync(income);
            Assert.That(actualTax, Is.EqualTo(expectedTax));
        }
    }
}