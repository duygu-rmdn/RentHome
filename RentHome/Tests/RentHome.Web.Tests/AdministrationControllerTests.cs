namespace RentHome.Web.Tests
{
    using MyTested.AspNetCore.Mvc;
    using RentHome.Web.Areas.Administration.Controllers;
    using RentHome.Web.ViewModels.Properties;
    using Xunit;

    using static RentHome.Web.Tests.Data.LocationData;
    using static RentHome.Web.Tests.Data.PropertiesData;

    public class AdministrationControllerTests
    {
        [Fact]
        public void IndexShouldReturnView()
            => MyController<LocationController>
                .Instance()
            .Calling(x => x.Index())
            .ShouldReturn()
            .View();

        [Fact]
        public void CreatePostMothodShoulReturnCorrectViewModel()
            => MyController<LocationController>
            .Instance(controller => controller.WithData())
            .Calling(x => x.Index(LocationIndexFormModel()))
            .ShouldHave()
            .ValidModelState()
            .AndAlso()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void ChangeVisibilityShouldReturnCorrectViewModel()
            => MyController<PropertiesController>
                .Instance(contoller => contoller.WithData(Property()))
            .Calling(x => x.ChangeVisibility("1"))
            .ShouldReturn();

        [Fact]
        public void LocationIndexShouldReturnCorrectViewModel()
            => MyController<PropertiesController>
                .Instance()
            .Calling(x => x.Index())
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<PropertiesListViewModel>());
    }
}
