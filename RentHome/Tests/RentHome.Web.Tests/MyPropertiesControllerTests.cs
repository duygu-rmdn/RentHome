namespace RentHome.Services.Data.Tests
{
    using MyTested.AspNetCore.Mvc;
    using RentHome.Web.Controllers;
    using RentHome.Web.ViewModels.Properties;
    using Xunit;

    public class MyPropertiesControllerTests
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<MyPropertiesController>
                .Instance(controller => controller.WithUser(u => u.WithIdentifier("1")))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<PropertiesListViewModel>());
    }
}
