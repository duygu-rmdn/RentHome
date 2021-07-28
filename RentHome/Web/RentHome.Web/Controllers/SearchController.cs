namespace RentHome.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RentHome.Services.Data;
    using RentHome.Web.ViewModels.Properties;
    using RentHome.Web.ViewModels.Search;

    public class SearchController : BaseController
    {
        private readonly IPropertyService propertyService;
        private readonly ICityService cityService;
        private readonly ICountryService countryService;

        public SearchController(
            IPropertyService propertyService,
            ICityService cityService,
            ICountryService countryService)
        {
            this.propertyService = propertyService;
            this.cityService = cityService;
            this.countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            var cities = await this.cityService.AllCitiesAsync();
            var countries = await this.countryService.AllCountriesAsync();

            var model = new SearchIndexViewModel
            {
                CityList = cities,
                CountryList = countries,
            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult List(SearchListInputModel input)
        {
            var viewModel = new ListViewModel()
            {
                Properties = this.propertyService.Search(input),
            };
            return this.View(viewModel);
        }
    }
}
