namespace RentHome.Web.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using RentHome.Data.Models;
    using RentHome.Web.Controllers;
    using RentHome.Web.ViewModels.Home;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.Index())
            .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IndexViewModel>());

        [Fact]
        public void PrivacyShouldReturnView()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.Privacy())
            .ShouldReturn()
            .View();
    }
}
