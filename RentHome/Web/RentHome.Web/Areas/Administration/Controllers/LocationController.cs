namespace RentHome.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RentHome.Web.Areas.Administration.Services;
    using RentHome.Web.ViewModels.Administration.Location;

    public class LocationController : AdministrationController
    {
        private readonly IAdminPropertyService service;

        public LocationController(IAdminPropertyService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LocationIndexFormModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.service.AddCityWithCountry(input);

            return this.Redirect("/");
        }
    }
}
