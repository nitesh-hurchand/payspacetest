using PaySpace.Test.TaxCalculatorWeb.Services;

namespace PaySpace.Test.TaxCalculatorWeb.Tests
{
    public class FlatRateTaxCalculationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TaxRateShouldBe17Dot5PercentForAllIncome()
        {
            var taxCalculator = new FlatRateTaxCalculator();

            var taxOnIncome1000000 = await taxCalculator.CalculateAsync(1000000);
            Assert.That(taxOnIncome1000000, Is.EqualTo(Math.Round(1000000 * 0.175, 2)));
        }
    }
}