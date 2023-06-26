namespace PaySpace.Test.TaxCalculatorWeb.Models
{
    public class ProgressiveTaxRateConfiguration
    {
        public Guid Id { get; set; }
        public decimal Rate { get; set; }
        public decimal FromIncome { get; set; }
        public decimal ToIncome { get; set; }
    }
}
