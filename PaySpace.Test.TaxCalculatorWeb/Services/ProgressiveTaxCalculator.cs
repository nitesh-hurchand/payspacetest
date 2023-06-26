using Microsoft.EntityFrameworkCore;
using PaySpace.Test.TaxCalculatorWeb.Data;
using PaySpace.Test.TaxCalculatorWeb.Models;

namespace PaySpace.Test.TaxCalculatorWeb.Services
{
    public class ProgressiveTaxCalculator : ITaxCalculator
    {
        private readonly TaxDbContext dbContext;

        public ProgressiveTaxCalculator(TaxDbContext dbContext) 
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<decimal> CalculateAsync(decimal income)
        {
            try
            {
                //todo - cache dataset below
                var taxConfigs = await dbContext.Set<ProgressiveTaxRateConfiguration>().Where(e => 
                (e.FromIncome < income && e.ToIncome < income) ||
                (e.FromIncome <= income && e.ToIncome >= income))
                    .OrderBy(e => e.FromIncome).ToListAsync();

                var taxCalcs = new List<Tuple<decimal, decimal, decimal>>();
                taxConfigs.ForEach(taxConfig =>
                {
                    if (taxConfig.ToIncome == SeedData.MaxDecimal) //last band
                    {
                        var chargeableIncome = income - taxConfig.FromIncome;
                        if (income == taxConfig.FromIncome) { chargeableIncome += 1; }
                        var rate = taxConfig.Rate;
                        var tax = Math.Round(chargeableIncome * rate, 2);
                        taxCalcs.Add(new Tuple<decimal, decimal, decimal>(taxConfig.Rate, chargeableIncome, tax));
                    }
                    else if (income >= taxConfig.FromIncome && income <= taxConfig.ToIncome) //within band
                    {
                        var chargeableIncome = income - taxConfig.FromIncome;
                        if (income == taxConfig.FromIncome) { chargeableIncome += 1; }
                        var rate = taxConfig.Rate;
                        var tax = Math.Round(chargeableIncome * rate, 2);
                        taxCalcs.Add(new Tuple<decimal, decimal, decimal>(taxConfig.Rate, chargeableIncome, tax));
                    }
                    else if (income > taxConfig.ToIncome) //beyond upper band -- this should not happen
                    {
                        var chargeableIncome = taxConfig.ToIncome - taxConfig.FromIncome;
                        var rate = taxConfig.Rate;
                        var tax = Math.Round(chargeableIncome * rate, 2);
                        taxCalcs.Add(new Tuple<decimal, decimal, decimal>(taxConfig.Rate, chargeableIncome, tax));
                    }
                    else if (income < taxConfig.FromIncome) //under lower band -- this should not happen
                    {
                        
                    }
                    else //--this should not happen
                    {
                    }
                });
                var tax = taxCalcs.Sum(x => x.Item3);
                return tax;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
