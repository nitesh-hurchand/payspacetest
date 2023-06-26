using PaySpace.Test.TaxCalculatorWeb.Models;

namespace PaySpace.Test.TaxCalculatorWeb.ViewModels.Home
{
    public class TaxConfigurationsViewModel
    {
        public IEnumerable<ProgressiveTaxRateConfiguration>? ProgressiveTaxRateConfigurations { get; set; }
    }
}
