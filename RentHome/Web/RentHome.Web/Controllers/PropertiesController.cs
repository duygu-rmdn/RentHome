namespace RentHome.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RentHome.Data.Models;
    using RentHome.Services.Data;
    using RentHome.Web.ViewModels.Properties;

    public class PropertiesController : Controller
    {
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        private readonly IPropertyService propertyService;
        private readonly UserManager<ApplicationUser> userManager;

        public PropertiesController(
            ICityService cityService,
            ICountryService countryService,
            IPropertyService propertyService,
            UserManager<ApplicationUser> userManager)
        {
            this.cityService = cityService;
            this.countryService = countryService;
            this.propertyService = propertyService;
            this.userManager = userManager;
        }

        [ActionName("GetCity")]
        public async Task<IActionResult> GetCityAsync(int id)
        {
            var model = await this.cityService.AllCitiesByCountryAsync(id);

            return this.Json(new SelectList(model, "Id", "Name"));
        }

        public async Task<IActionResult> Create()
        {
            var cities = await this.cityService.AllCitiesAsync();
            var countries = await this.countryService.AllCountriesAsync();

            var model = new CreatePropertyInputModel
            {
                CityList = cities,
                CountryList = countries,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.propertyService.CreateAsync(input, user.Id);

            // TODO: redirect to Property info page
            return this.Redirect("/");
        }
    }
}
