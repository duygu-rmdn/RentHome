namespace RentHome.Web.Tests
{
    using System;

    using MyTested.AspNetCore.Mvc;
    using RentHome.Data.Models;
    using RentHome.Data.Models.Enums;
    using RentHome.Web.Controllers;
    using RentHome.Web.ViewModels.Properties;
    using RentHome.Web.ViewModels.Rental;
    using Xunit;

    using static RentHome.Web.Tests.Data.PropertiesData;
    using static RentHome.Web.Tests.Data.RentalData;

    public class RentalControllerTests
    {
        [Fact]
        public void IndexShouldReturnCorrectViewModel()
            => MyController<RentalController>
            .Instance()
            .Calling(x => x.Index("1"))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<RequestInputModel>());

        [Fact]
        public void IndexPostMoethodShouldReturnCorrectViewModel()
            => MyController<RentalController>
            .Instance()
            .Calling(x => x.Index(RequestInputModel(), "1"))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));

        [Fact]
        public void ApproveShoulReturnCorrectViewModel()
            => MyController<RentalController>
            .Instance(controller => controller.WithData(Property(), Request()))
            .Calling(x => x.Approve("1", "1"));

        [Fact]
        public void RejectShoulReturnCorrectViewModel()
            => MyController<RentalController>
            .Instance(controller => controller.WithData(Property(), Request()))
            .Calling(x => x.Rejected("1", "1"));
    }
}
