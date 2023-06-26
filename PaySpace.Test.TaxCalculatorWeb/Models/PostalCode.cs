namespace PaySpace.Test.TaxCalculatorWeb.Models
{
    public class PostalCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public TaxCalculationType TaxCalculationType { get; set; }
    }
}
