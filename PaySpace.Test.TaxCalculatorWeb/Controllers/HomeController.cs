using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaySpace.Test.TaxCalculatorWeb.Data;
using PaySpace.Test.TaxCalculatorWeb.Models;
using PaySpace.Test.TaxCalculatorWeb.ViewModels.Home;
using System.Diagnostics;

namespace PaySpace.Test.TaxCalculatorWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaxDbContext taxDbContext;

        public HomeController(ILogger<HomeController> logger,
            TaxDbContext taxDbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.taxDbContext = taxDbContext ?? throw new ArgumentNullException(nameof(taxDbContext));
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel();
            viewModel.PostalCodes = await taxDbContext.Set<PostalCode>().ToListAsync();
            return View(viewModel);
        }
        public async Task<IActionResult> TaxCalculations()
        {
            var viewModel = new TaxCalculationsViewModel();
            viewModel.TaxCalculations = await taxDbContext.Set<TaxCalculation>().Include(e => e.PostalCode).OrderBy(e => e.CalculatedOn).ToListAsync();
            return View(viewModel);
        }
        public async Task<IActionResult> TaxConfigurations()
        {
            var viewModel = new TaxConfigurationsViewModel();
            viewModel.ProgressiveTaxRateConfigurations = await taxDbContext.Set<ProgressiveTaxRateConfiguration>().OrderBy(e => e.FromIncome).ToListAsync();
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}