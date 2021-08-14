namespace RentHome.Web.Tests
{
    using MyTested.AspNetCore.Mvc;
    using RentHome.Web.Controllers;
    using Xunit;

    using static RentHome.Web.Tests.Data.ContactUsData;

    public class ContactUsContollerTests
    {
        [Fact]
        public void IndexShouldReturnView()
           => MyController<ContactUsController>
           .Instance()
           .Calling(c => c.Index())
           .ShouldReturn()
           .View();

        [Fact]
        public void IndexShouldHaveValidModelState()
           => MyController<ContactUsController>
           .Instance()
           .Calling(c => c.Index(ContactData()))
           .ShouldHave()
           .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .Redirect(result => result
                .To<HomeController>(c => c.Index()));
    }
}
