using PaySpace.Test.TaxCalculatorWeb.Models;

namespace PaySpace.Test.TaxCalculatorWeb.Services
{
    public interface ITaxCalculatorFactory
    {
        ITaxCalculator GetCalculator(TaxCalculationType taxCalculationType);
    }

    public class TaxCalculatorFactory : ITaxCalculatorFactory
    {
        private readonly IServiceProvider serviceProvider;

        public TaxCalculatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        public ITaxCalculator GetCalculator(TaxCalculationType taxCalculationType)
        {
            switch (taxCalculationType)
            {
                case TaxCalculationType.FlatValue:
                    return serviceProvider.GetRequiredService<FlatValueTaxCalculator>();
                case TaxCalculationType.FlatRate:
                    return serviceProvider.GetRequiredService<FlatRateTaxCalculator>();
                case TaxCalculationType.Progressive:
                    return serviceProvider.GetRequiredService<ProgressiveTaxCalculator>();
                default:
                    throw new NotImplementedException($"Tax Calculation Type {taxCalculationType} is not supported.");
            }
        }
    }
}
