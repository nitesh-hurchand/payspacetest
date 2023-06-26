using Microsoft.AspNetCore.Mvc;
using PaySpace.Test.TaxCalculatorWeb.Services;

namespace PaySpace.Test.TaxCalculatorWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ILogger<TaxController> logger;
        private readonly ITaxCalculationTypeResolver taxCalculationTypeResolver;
        private readonly TaxCalculator taxCalculator;

        public TaxController(ILogger<TaxController> logger,
            ITaxCalculationTypeResolver taxCalculationTypeResolver,
            TaxCalculator taxCalculator) 
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.taxCalculationTypeResolver = taxCalculationTypeResolver ?? throw new ArgumentNullException(nameof(taxCalculationTypeResolver));
            this.taxCalculator = taxCalculator ?? throw new ArgumentNullException(nameof(taxCalculator));
        }
        // POST: api/tax
        public async Task<TaxResult> Post([FromBody] TaxRequest taxRequest)
        {
            if (taxRequest == null) { return new TaxResult { Success = false, Error = $"Invalid request object" }; }
            if (taxRequest.PostalCodeId == Guid.Empty) { return new TaxResult { Success = false, Error = $"Invalid post code id {taxRequest.PostalCodeId}" }; }
            if (taxRequest.Income <= 0) { return new TaxResult { Success = false, Error = $"Invalid income {taxRequest.Income}. Income should be greater than 0." }; }
            try
            {
                var postalCode = await taxCalculationTypeResolver.ResolveAsync(taxRequest.PostalCodeId);

                if (postalCode == null) { return new TaxResult { Success = false, Error = $"Invalid post code id {taxRequest.PostalCodeId}" }; }

                var taxAmount = await taxCalculator.CalculateAsync(postalCode, taxRequest.Income);
                return new TaxResult { Success = true, Tax = taxAmount };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error has occurred when calculating tax for {postalCodeId} and income {income} - {error}", taxRequest.PostalCodeId, taxRequest.Income, ex.Message);
                return new TaxResult { Success = false, Error = $"An error has occurred when calculating tax for {taxRequest.PostalCodeId} and income {taxRequest.Income} - {ex.Message}" };
            }
        }
    }

    public class TaxRequest
    {
        public Guid PostalCodeId { get; set; }
        public decimal Income { get; set; }
    }

    public class TaxResult
    {
        public bool Success { get; set; }
        public decimal? Tax { get; set; }  
        public string? Error { get; set; }
    }
}
