using PaySpace.Test.TaxCalculatorWeb.Services;

namespace PaySpace.Test.TaxCalculatorWeb.Tests
{
    public class FlatValueTaxCalculationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TaxShouldBe10000WhenIncomeIsGreaterOrEqualTo200000()
        {
            var taxCalculator = new FlatValueTaxCalculator();

            var taxOnIncome200000 = await taxCalculator.CalculateAsync(200000);
            Assert.That(taxOnIncome200000, Is.EqualTo(10000));

            var taxOnIncome3000000 = await taxCalculator.CalculateAsync(3000000);
            Assert.That(taxOnIncome3000000, Is.EqualTo(10000));
        }

        [Test]
        public async Task TaxRateShouldBe5PercentWhenIncomeIsLessThan200000()
        {
            var taxCalculator = new FlatValueTaxCalculator();

            var taxOnIncome199999 = await taxCalculator.CalculateAsync(199999);
            Assert.That(taxOnIncome199999, Is.EqualTo(Math.Round(199999 * 0.05, 2)));
        }
    }
}