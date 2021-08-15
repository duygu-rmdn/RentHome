namespace RentHome.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RentHome.Common;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Services.Data;
    using RentHome.Web.ViewModels.Properties;

    public class PropertiesController : BaseController
    {
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        private readonly IPropertyService propertyService;
        private readonly IRentalService rentalService;
        private readonly IRepository<Property> propertyRepository;
        private readonly IWebHostEnvironment environment;
        private readonly IContactService contactService;
        private readonly UserManager<ApplicationUser> userManager;

        public PropertiesController(
            ICityService cityService,
            ICountryService countryService,
            IPropertyService propertyService,
            IRentalService rentalService,
            IRepository<Property> propertyRepository,
            IWebHostEnvironment environment,
            IContactService contactService,
            UserManager<ApplicationUser> userManager)
        {
            this.cityService = cityService;
            this.countryService = countryService;
            this.propertyService = propertyService;
            this.rentalService = rentalService;
            this.propertyRepository = propertyRepository;
            this.environment = environment;
            this.contactService = contactService;
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
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ownerId = this.propertyRepository.All()
                .Where(x => x.Id == id)
                .Select(x => x.OwnerId)
                .FirstOrDefault();

            var managerId = this.propertyRepository.All()
                .Where(x => x.Id == id)
                .Select(x => x.ManagerId)
                .FirstOrDefault();

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (userId != ownerId && userId != managerId && !this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.BadRequest();
            }

            await this.propertyService.UpdateAsync(id, input);

            this.TempData["Edit"] = "The changes were successful!";

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [Authorize]
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
        [Authorize]
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
                this.TempData["CreateFailed"] = "Failed to create property, allowed photo extensions are \"jpg\", \"png\", \"gif\"!";
                return this.Redirect("/");
            }

            this.TempData["Create"] = "You have successfully created a property and your property is awaiting inspection!";

            return this.Redirect("/");
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
            viewModel.Requests = this.rentalService.MyPropertyRequests(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Details(SinglePropertyViewModel input, string id)
        {
            var result = input.Contact;

            string ownerEmail = this.propertyRepository.All()
                .Where(x => x.Id == id)
                .Select(x => x.Owner.Email)
                .FirstOrDefault();

            this.contactService.ContactWithOwner(result, ownerEmail);

            this.TempData["Contact"] = "Your message was sended successful!";

            return this.Redirect("/");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await this.propertyService.DeleteAsync(id);

            this.TempData["Delete"] = "You have successfully deleted a property!";

            return this.Redirect("/Properties/All/1");
        }
    }
}
