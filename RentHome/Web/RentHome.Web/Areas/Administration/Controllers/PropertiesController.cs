namespace RentHome.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentHome.Web.Areas.Administration.Services;
    using RentHome.Web.ViewModels.Properties;

    public class PropertiesController : AdministrationController
    {
        private readonly IAdminPropertyService adminPropertyService;

        public PropertiesController(IAdminPropertyService adminPropertyService)
        {
            this.adminPropertyService = adminPropertyService;
        }

        public IActionResult Index()
        {
            var viewModel = new PropertiesListViewModel
            {
                Properties = this.adminPropertyService.ShowNewProperties(),
            };
            return this.View(viewModel);
        }
    }
}
