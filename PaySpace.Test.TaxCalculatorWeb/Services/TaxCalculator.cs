using PaySpace.Test.TaxCalculatorWeb.Data;
using PaySpace.Test.TaxCalculatorWeb.Models;

namespace PaySpace.Test.TaxCalculatorWeb.Services
{
    public class TaxCalculator
    {
        private readonly ILogger<TaxCalculator> logger;
        private readonly ITaxCalculationTypeResolver taxCalculationTypeResolver;
        private readonly ITaxCalculatorFactory taxCalculatorFactory;
        private readonly TaxDbContext dbContext;

        public TaxCalculator(ILogger<TaxCalculator> logger,
            ITaxCalculationTypeResolver taxCalculationTypeResolver,
            ITaxCalculatorFactory taxCalculatorFactory,
            TaxDbContext dbContext) 
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.taxCalculationTypeResolver = taxCalculationTypeResolver ?? throw new ArgumentNullException(nameof(taxCalculationTypeResolver));
            this.taxCalculatorFactory = taxCalculatorFactory ?? throw new ArgumentNullException(nameof(taxCalculatorFactory));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<decimal> CalculateAsync(PostalCode postalCode, decimal income)
        {
            if (postalCode == null) { throw new ArgumentNullException(nameof(postalCode)); }

            try
            {
                var taxCalculator = taxCalculatorFactory.GetCalculator(postalCode.TaxCalculationType);
                if (taxCalculator == null) { throw new Exception($"Tax calculator could not be determined for {postalCode.TaxCalculationType}"); }

                var tax = await taxCalculator.CalculateAsync(income);

                var taxCalculation = new TaxCalculation 
                {
                    Id = Guid.NewGuid(),
                    CalculatedOn = DateTimeOffset.UtcNow,
                    PostalCode = postalCode,
                    Income = income,
                    TaxAmount = tax
                };
                await dbContext.AddAsync(taxCalculation);
                await dbContext.SaveChangesAsync();
                return tax;
            }
            catch (Exception ex)
            {
                //log exception
                logger.LogError(ex, "An error has occurred whe calculating tax for postalCodeId {postalCodeId} and income {income}", postalCode.Id, income);
                throw;
            }
        }
    }
}
