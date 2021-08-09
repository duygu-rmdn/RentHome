namespace RentHome.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentHome.Services.Data;
    using RentHome.Web.ViewModels.Rental;

    public class RentalController : BaseController
    {
        private readonly IRentalService rentalService;

        public RentalController(IRentalService rentalService)
        {
            this.rentalService = rentalService;
        }

        [Authorize]
        public IActionResult Index(string id)
        {
            var property = this.rentalService.GetProperty(id);
            var viewModel = new RequestInputModel
            {
                Property = property,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(RequestInputModel input, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.rentalService.RequestAsync(input, userId, id);
            return this.Redirect("/");
        }
    }
}
