using PaySpace.Test.TaxCalculatorWeb.Models;

namespace PaySpace.Test.TaxCalculatorWeb.Services
{
    public interface ITaxCalculator
    {
        Task<decimal> CalculateAsync(decimal income);
    }
}