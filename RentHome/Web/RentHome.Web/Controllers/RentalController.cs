namespace RentHome.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Services.Data;
    using RentHome.Web.ViewModels.Rental;

    public class RentalController : BaseController
    {
        private readonly IRentalService rentalService;
        private readonly IPropertyService propertyService;

        public RentalController(
            IRentalService rentalService,
            IPropertyService propertyService)
        {
            this.rentalService = rentalService;
            this.propertyService = propertyService;
        }

        [Authorize]
        public IActionResult Index(string id)
        {
            var property = this.rentalService.GetProperty(id);
            var propertyStatus = this.propertyService.GetStatusById(id);
            var viewModel = new RequestInputModel
            {
                Property = property,
                Status = propertyStatus,
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

            this.TempData["Request"] = "Your request was sended successful!";

            return this.Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> Approve(string propertyId, string requestId)
        {
            await this.rentalService.ApproveAsync(propertyId, requestId);

            this.TempData["Approve"] = "Your approved is successful!";

            return this.RedirectToAction("Details", "Properties", new { id = propertyId });
        }

        [Authorize]
        public async Task<IActionResult> Rejected(string id, string propertyId)
        {
            await this.rentalService.RejectedAsync(id);

            this.TempData["Reject"] = "Your rejection is successful!";

            return this.RedirectToAction("Details", "Properties", new { id = propertyId });
        }

        [Authorize]
        public async Task<IActionResult> Contract(string propertyId, string requestId)
        {
            var viewModel = await this.rentalService.GetContractInfoAsync(propertyId, requestId);
            return this.View(viewModel);
        }
    }
}
