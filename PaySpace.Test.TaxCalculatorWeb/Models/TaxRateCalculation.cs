namespace PaySpace.Test.TaxCalculatorWeb.Models
{
    public class TaxCalculation
    {
        public Guid Id { get; set; }
        public PostalCode? PostalCode { get; set; }
        public decimal Income { get; set; }
        public DateTimeOffset CalculatedOn { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
