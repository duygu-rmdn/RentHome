namespace RentHome.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Services.Data;
    using RentHome.Web.ViewModels.Properties;

    public class PropertiesController : BaseController
    {
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        private readonly IPropertyService propertyService;
        private readonly IRepository<Property> propertyRepository;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<ApplicationUser> userManager;

        public PropertiesController(
            ICityService cityService,
            ICountryService countryService,
            IPropertyService propertyService,
            IRepository<Property> propertyRepository,
            IWebHostEnvironment environment,
            UserManager<ApplicationUser> userManager)
        {
            this.cityService = cityService;
            this.countryService = countryService;
            this.propertyService = propertyService;
            this.propertyRepository = propertyRepository;
            this.environment = environment;
            this.userManager = userManager;
        }

        [ActionName("GetCity")]
        public async Task<IActionResult> GetCityAsync(int id)
        {
            var model = await this.cityService.AllCitiesByCountryAsync(id);

            return this.Json(new SelectList(model, "Id", "Name"));
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var inputModel = this.propertyService.GetById(id);
            return this.View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditPropertyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.propertyService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.Details), new { id });
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
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.propertyService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            string propertyId = this.propertyRepository.All()
                .Where(x => x.Address == input.Address)
                .Select(x => x.Id)
                .FirstOrDefault();

            return this.Redirect($"/Properties/Details/{propertyId}");
        }

        public IActionResult All(int id = 1)
        {
            const int ItemsPerPage = 9;
            var viewModel = new PropertiesListViewModel
            {
                PageNumber = id,
                Properties = this.propertyService.GetAll(id, ItemsPerPage),
                PropertiesCount = this.propertyService.GetCount(),
                ItemsPerPage = ItemsPerPage,
            };
            return this.View(viewModel);
        }

        public IActionResult Details(string id)
        {
            var viewModel = this.propertyService.GetSingleProperty(id);
            return this.View(viewModel);
        }
    }
}
