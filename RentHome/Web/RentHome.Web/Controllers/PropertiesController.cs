namespace RentHome.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentHome.Services.Data;
    using RentHome.Web.ViewModels.Properties;
    using System.Threading.Tasks;

    public class PropertiesController : Controller
    {
        private readonly ICityService cityService;
        private readonly ICountryService countryService;

        public PropertiesController(
            ICityService cityService,
            ICountryService countryService)
        {
            this.cityService = cityService;
            this.countryService = countryService;
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
        public IActionResult Create(CreatePropertyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // TODO: redirect to Property info page
            // TODO: Create Property using service method
            return this.Redirect("/");
        }
    }
}
