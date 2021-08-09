namespace RentHome.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class MyPropertiesController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
