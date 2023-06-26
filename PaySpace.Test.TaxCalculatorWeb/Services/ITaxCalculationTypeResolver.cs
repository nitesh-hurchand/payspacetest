using PaySpace.Test.TaxCalculatorWeb.Data;
using PaySpace.Test.TaxCalculatorWeb.Models;

namespace PaySpace.Test.TaxCalculatorWeb.Services
{
    public interface ITaxCalculationTypeResolver
    {
        Task<PostalCode> ResolveAsync(Guid postalCodeId);
    }

    public class TaxCalculationTypeResolver : ITaxCalculationTypeResolver
    {
        private readonly TaxDbContext dbContext;

        public TaxCalculationTypeResolver(TaxDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<PostalCode> ResolveAsync(Guid postalCodeId)
        {
            var postalCode = await dbContext.Set<PostalCode>().FindAsync(postalCodeId);

            if (postalCode == null) { throw new Exception($"Invalid post code id {postalCodeId}"); }

            return postalCode;
        }
    }
}
