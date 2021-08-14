namespace RentHome.Web.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class SearchPageTests
    {
        [Fact]
        public async Task SearchPageShouldReturnSuccessStatus()
        {
            var wepApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient client = wepApplicationFactory.CreateClient();

            var response = await client.GetAsync("/Search");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task SearchPageShouldContaionListingsHeading()
        {
            var wepApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient client = wepApplicationFactory.CreateClient();

            var response = await client.GetAsync("/Search");

            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("<title>Search - RentHome</title>", html);
        }
    }
}
