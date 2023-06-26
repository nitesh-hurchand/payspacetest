using PaySpace.Test.TaxCalculatorWeb.Models;

namespace PaySpace.Test.TaxCalculatorWeb.Data
{
    public class SeedData
    {
        private readonly ILogger<SeedData>? logger;
        private readonly TaxDbContext dbContext;

        public const decimal MaxDecimal = 9999999999999999.99m;

        public SeedData(ILogger<SeedData>? logger, TaxDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext))!;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested) { return; }

            logger?.LogInformation("Adding seed data for postal codes and progressive tax configurations");

            try
            {
                await AddPostalCodes();
                await AddProgressiveTaxRateConfigurations();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Exception: {message}", ex.Message);
            }
        }

        private async Task AddPostalCodes()
        {
            var postalCodeId7441 = Guid.Parse("2dec427b-b62e-4d4b-b8d9-439479871040");
            var postalCode7441 = await dbContext.FindAsync<PostalCode>(postalCodeId7441);
            if (postalCode7441 == null)
            {
                await dbContext.AddAsync(new PostalCode
                {
                    Id = postalCodeId7441,
                    Code = "7441",
                    TaxCalculationType = TaxCalculationType.Progressive

                });
            }

            var postalCodeIdA100 = Guid.Parse("5e073b47-beca-4aaa-a9b3-0eb94eb2c9ec");
            var postalCodeA100 = await dbContext.FindAsync<PostalCode>(postalCodeIdA100);
            if (postalCodeA100 == null)
            {
                await dbContext.AddAsync(new PostalCode
                {
                    Id = postalCodeIdA100,
                    Code = "A100",
                    TaxCalculationType = TaxCalculationType.FlatValue

                });
            }

            var postalCodeId7000 = Guid.Parse("88878e22-3375-4a73-88d6-e77cf43253a7");
            var postalCode7000 = await dbContext.FindAsync<PostalCode>(postalCodeId7000);
            if (postalCode7000 == null)
            {
                await dbContext.AddAsync(new PostalCode
                {
                    Id = postalCodeId7000,
                    Code = "7000",
                    TaxCalculationType = TaxCalculationType.FlatRate

                });
            }

            var postalCodeId1000 = Guid.Parse("06f5553a-c010-4354-9d88-d09743edb763");
            var postalCode1000 = await dbContext.FindAsync<PostalCode>(postalCodeId1000);
            if (postalCode1000 == null)
            {
                await dbContext.AddAsync(new PostalCode
                {
                    Id = postalCodeId1000,
                    Code = "1000",
                    TaxCalculationType = TaxCalculationType.Progressive

                });
            }
            await dbContext.SaveChangesAsync();
        }

        private async Task AddProgressiveTaxRateConfigurations()
        {
            var progressiveTaxId0To8350 = Guid.Parse("a011aec3-78fb-4d70-ad67-03d70036fde7");
            var progressiveTax0To8350 = await dbContext.FindAsync<ProgressiveTaxRateConfiguration>(progressiveTaxId0To8350);
            if (progressiveTax0To8350 == null)
            {
                await dbContext.AddAsync(new ProgressiveTaxRateConfiguration
                {
                    Id = progressiveTaxId0To8350,
                    Rate = 0.1m,
                    FromIncome = 0,
                    ToIncome = 8350

                });
            }

            var progressiveTaxId8351To33950 = Guid.Parse("5cf899ed-e815-4e14-a6af-95e1394ece5b");
            var progressiveTax8351To33950 = await dbContext.FindAsync<ProgressiveTaxRateConfiguration>(progressiveTaxId8351To33950);
            if (progressiveTax8351To33950 == null)
            {
                await dbContext.AddAsync(new ProgressiveTaxRateConfiguration
                {
                    Id = progressiveTaxId8351To33950,
                    Rate = 0.15m,
                    FromIncome = 8351,
                    ToIncome = 33950

                });
            }

            var progressiveTaxId33951To82250 = Guid.Parse("05ad5991-7824-475c-8ce4-c92cec6d2eb8");
            var progressiveTax33951To82250 = await dbContext.FindAsync<ProgressiveTaxRateConfiguration>(progressiveTaxId33951To82250);
            if (progressiveTax33951To82250 == null)
            {
                await dbContext.AddAsync(new ProgressiveTaxRateConfiguration
                {
                    Id = progressiveTaxId33951To82250,
                    Rate = 0.25m,
                    FromIncome = 33951,
                    ToIncome = 82250

                });
            }

            var progressiveTaxId82251To171550 = Guid.Parse("64d6d7d8-5d1c-4d9a-befb-97ae9311a2ff");
            var progressiveTax82251To171550 = await dbContext.FindAsync<ProgressiveTaxRateConfiguration>(progressiveTaxId82251To171550);
            if (progressiveTax82251To171550 == null)
            {
                await dbContext.AddAsync(new ProgressiveTaxRateConfiguration
                {
                    Id = progressiveTaxId82251To171550,
                    Rate = 0.28m,
                    FromIncome = 82251,
                    ToIncome = 171550

                });
            }

            var progressiveTaxId171551To372950 = Guid.Parse("a8e0d81f-847d-4d16-954a-d834eb8c4f1d");
            var progressiveTax171551To372950 = await dbContext.FindAsync<ProgressiveTaxRateConfiguration>(progressiveTaxId171551To372950);
            if (progressiveTax171551To372950 == null)
            {
                await dbContext.AddAsync(new ProgressiveTaxRateConfiguration
                {
                    Id = progressiveTaxId171551To372950,
                    Rate = 0.33m,
                    FromIncome = 171551,
                    ToIncome = 372950

                });
            }

            var progressiveTaxId372951ToUndefined = Guid.Parse("0aa02a86-b186-4d58-be7c-098505f8dd9f");
            var progressiveTax372951ToUndefined = await dbContext.FindAsync<ProgressiveTaxRateConfiguration>(progressiveTaxId372951ToUndefined);
            if (progressiveTax372951ToUndefined == null)
            {
                await dbContext.AddAsync(new ProgressiveTaxRateConfiguration
                {
                    Id = progressiveTaxId372951ToUndefined,
                    Rate = 0.35m,
                    FromIncome = 372951,
                    ToIncome = MaxDecimal
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
