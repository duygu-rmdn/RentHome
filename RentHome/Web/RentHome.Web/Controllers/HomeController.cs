namespace RentHome.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using RentHome.Data;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Services.Data;
    using RentHome.Web.ViewModels;
    using RentHome.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IPropertyService propertyService;

        public HomeController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                RandomProperties = this.propertyService.GetRandom(3),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
