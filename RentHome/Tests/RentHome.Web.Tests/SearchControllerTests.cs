namespace RentHome.Web.Tests
{
    using MyTested.AspNetCore.Mvc;
    using RentHome.Data.Models.Enums;
    using RentHome.Web.Controllers;
    using RentHome.Web.ViewModels.Search;
    using Xunit;

    using static RentHome.Web.Tests.Data.SearchData;

    public class SearchControllerTests
    {
        [Fact]
        public void IndexShouldReturnCorrectIndexViewModel()
            => MyController<SearchController>
            .Instance()
            .Calling(x => x.Index())
            .ShouldReturn()
            .View(view => view
                    .WithModelOfType<SearchIndexViewModel>());

        [Fact]
        public void ListShouldReturnCorrectViewModel()
            => MyController<SearchController>
            .Instance()
            .Calling(x => x.List(SearchListInputModel()))
            .ShouldReturn()
            .View(view => view
                    .WithModelOfType<ListViewModel>());
    }
}
