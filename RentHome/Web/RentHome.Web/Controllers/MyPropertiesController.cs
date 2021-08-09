namespace RentHome.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentHome.Services.Data;
    using RentHome.Web.ViewModels.Properties;

    [Authorize]
    public class MyPropertiesController : BaseController
    {
        private readonly IMyPropertiesService mypropertyService;

        public MyPropertiesController(IMyPropertiesService mypropertyService)
        {
            this.mypropertyService = mypropertyService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new PropertiesListViewModel
            {
                Properties = this.mypropertyService.GetMyProperties(userId),
            };

            return this.View(viewModel);
        }
    }
}
