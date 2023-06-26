using Microsoft.EntityFrameworkCore;
using PaySpace.Test.TaxCalculatorWeb.Data;
using PaySpace.Test.TaxCalculatorWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TaxDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaxDbContext")));
builder.Services.AddScoped<SeedData>();
builder.Services.AddTransient<TaxCalculator>();
builder.Services.AddScoped<ITaxCalculatorFactory, TaxCalculatorFactory>();
builder.Services.AddScoped<FlatValueTaxCalculator>();
builder.Services.AddScoped<FlatRateTaxCalculator>();
builder.Services.AddScoped<ProgressiveTaxCalculator>();
builder.Services.AddScoped<ITaxCalculationTypeResolver, TaxCalculationTypeResolver>();

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dbContext = serviceScope.ServiceProvider.GetRequiredService<TaxDbContext>();

var created = await dbContext.Database.EnsureCreatedAsync();
if (created)
{
    var seedData = serviceScope.ServiceProvider.GetRequiredService<SeedData>();
    await seedData.SeedAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
