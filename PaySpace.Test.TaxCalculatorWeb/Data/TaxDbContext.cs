using Microsoft.EntityFrameworkCore;
using PaySpace.Test.TaxCalculatorWeb.Models;

namespace PaySpace.Test.TaxCalculatorWeb.Data
{
    public class TaxDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the db context.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public TaxDbContext(DbContextOptions<TaxDbContext> options)
            : base(options)
        {
            ArgumentNullException.ThrowIfNull(nameof(options));
            if (!options.ContextType.IsAssignableFrom(GetType()))
            {
                throw new InvalidOperationException();
            }
            Options = options;
        }
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        protected TaxDbContext() : base() 
        {

            Options = new DbContextOptions<TaxDbContext>();
        }

        public DbContextOptions<TaxDbContext> Options { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostalCode>(b =>
            {
                b.ToTable("PostalCodes"); 
                b.HasKey(e => e.Id);
                b.Property(e => e.Code).IsRequired().HasMaxLength(4);
                b.Property(e => e.TaxCalculationType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasConversion(
                        v => v.ToString(),
                        v => (TaxCalculationType)Enum.Parse(typeof(TaxCalculationType), v));
            });

            builder.Entity<ProgressiveTaxRateConfiguration>(b =>
            {
                b.ToTable("ProgressiveTaxRateConfigurations");
                b.HasKey(e => e.Id);
                b.Property(e => e.Rate).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(e => e.FromIncome).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(e => e.ToIncome).HasColumnType("decimal(18,2)");
            });


            builder.Entity<TaxCalculation>(b =>
            {
                b.ToTable("TaxCalculations");
                b.HasKey(e => e.Id);
                b.Property(e => e.Income).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(e => e.CalculatedOn).IsRequired();
                b.Property(e => e.TaxAmount).IsRequired().HasColumnType("decimal(18,2)");
                b.HasOne(e => e.PostalCode).WithMany().IsRequired();
            });
        }
    }
}
