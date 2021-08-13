namespace RentHome.Web.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class AllPropertiesPageTests
    {
        [Fact]
        public async Task AllPageShouldReturnSuccessStatus()
        {
            var wepApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient client = wepApplicationFactory.CreateClient();

            var response = await client.GetAsync("/Properties/All");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AllPageShouldContaionListingsHeading()
        {
            var wepApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient client = wepApplicationFactory.CreateClient();

            var response = await client.GetAsync("/Properties/All");

            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("<title>All Properties - RentHome</title>", html);
        }
    }
}
