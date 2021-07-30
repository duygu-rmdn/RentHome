namespace RentHome.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentHome.Services.Data;
    using RentHome.Web.ViewModels.ContactUs;

    public class ContactUsController : BaseController
    {
        private readonly IContactService contactUsService;

        public ContactUsController(IContactService contactUsService)
        {
            this.contactUsService = contactUsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Index(ContactInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            this.contactUsService.Contact(input);

            return this.Redirect("/");
        }
    }
}
