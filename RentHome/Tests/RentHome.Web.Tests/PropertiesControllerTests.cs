namespace RentHome.Web.Tests
{
    using MyTested.AspNetCore.Mvc;
    using RentHome.Data.Models;
    using RentHome.Data.Models.Enums;
    using RentHome.Web.Controllers;
    using RentHome.Web.ViewModels.Properties;
    using Xunit;

    using static RentHome.Web.Tests.Data.PropertiesData;

    public class PropertiesControllerTests
    {
        [Fact]
        public void CreateShoulReturnCorrectViewModel()
            => MyController<PropertiesController>
            .Instance()
            .Calling(x => x.Create())
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<CreatePropertyInputModel>());

        [Fact]
        public void CreatePostMothodShoulReturnCorrectViewModel()
            => MyController<PropertiesController>
            .Instance(controller => controller.WithData(City(), Country()))
            .Calling(x => x.Create(CreatePropertyInputModel()))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void AllShoulReturnCorrectViewModel()
            => MyController<PropertiesController>
            .Instance(controller => controller.WithData(Property()))
            .Calling(x => x.All(1))
            .ShouldReturn()
                .View(view => view
                    .WithModelOfType<PropertiesListViewModel>());

        [Fact]
        public void DetailsShoulReturnCorrectViewModel()
        => MyController<PropertiesController>
        .Instance(controller => controller.WithData(Property()))
        .Calling(x => x.Details("1"));

        [Fact]
        public void DetailsPostShoulReturnCorrectViewModel()
        => MyController<PropertiesController>
        .Instance(controller => controller.WithData(Property()))
        .Calling(x => x.Details(SinglePropertyViewModel(), "1"));

        [Fact]
        public void EditShoulReturnCorrectViewModel()
            => MyController<PropertiesController>
            .Instance()
            .Calling(x => x.Edit("1"))
            .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void EditPostMothodShoulReturnCorrectViewModel()
            => MyController<PropertiesController>
            .Instance(controller => controller.WithData(Property()))
            .Calling(x => x.Edit("1", EditPropertyInputModel()))
            .ShouldHave()
            .ValidModelState()
            .AndAlso()
            .ShouldHave()
                .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void DeleteShoulReturnCorrectViewModel()
            => MyController<PropertiesController>
            .Instance(controller => controller.WithData(Property()))
            .Calling(x => x.Delete("1"))
            .ShouldHave()
                .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
    }
}
