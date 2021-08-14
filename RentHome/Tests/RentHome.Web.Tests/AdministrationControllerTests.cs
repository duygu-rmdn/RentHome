namespace RentHome.Web.Tests
{
    using MyTested.AspNetCore.Mvc;
    using RentHome.Web.Areas.Administration.Controllers;
    using RentHome.Web.ViewModels.Properties;
    using Xunit;

    using static RentHome.Web.Tests.Data.PropertiesData;

    public class AdministrationControllerTests
    {
        [Fact]
        public void IndexShouldReturnCorrectViewModel()
            => MyController<PropertiesController>
                .Instance()
            .Calling(x => x.Index())
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<PropertiesListViewModel>());

        [Fact]
        public void ChangeVisibilityShouldReturnCorrectViewModel()
            => MyController<PropertiesController>
                .Instance(contoller => contoller.WithData(Property()))
            .Calling(x => x.ChangeVisibility("1"))
            .ShouldReturn();
    }
}
