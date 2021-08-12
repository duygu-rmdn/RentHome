namespace RentHome.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PropertiesController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
