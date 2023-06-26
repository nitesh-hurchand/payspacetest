using PaySpace.Test.TaxCalculatorWeb.Models;

namespace PaySpace.Test.TaxCalculatorWeb.Services
{
    public class FlatValueTaxCalculator : ITaxCalculator
    {
        private const decimal RateBelowIncomeThreshold = 0.05m;
        private const decimal FixedTaxAmount = 10000m;
        private const decimal IncomeThreshold = 200000m;

        public FlatValueTaxCalculator() 
        {
        }
        public Task<decimal> CalculateAsync(decimal income)
        {
            if (income >= IncomeThreshold) { return Task.FromResult(FixedTaxAmount); }

            var tax = Math.Round(income * RateBelowIncomeThreshold, 2);
            return Task.FromResult(tax);
        }
    }
}
