namespace PaySpace.Test.TaxCalculatorWeb.Services
{
    public class FlatRateTaxCalculator : ITaxCalculator
    {
        private const decimal FixedTaxRate = 0.175m;

        public FlatRateTaxCalculator() 
        {
        }
        public Task<decimal> CalculateAsync(decimal income)
        {
            var tax = Math.Round(income * FixedTaxRate, 2);
            return Task.FromResult(tax);
        }
    }
}
