namespace RentHome.Web.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class HomePageTests
    {
        [Fact]
        public async Task HomePageShouldReturnSuccessStatus()
        {
            var wepApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient client = wepApplicationFactory.CreateClient();

            var response = await client.GetAsync("/");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task HomePageShouldContaionFuturedPropertiesHeading()
        {
            var wepApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient client = wepApplicationFactory.CreateClient();

            var response = await client.GetAsync("/");

            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("<h2>Featured Properties</h2>", html);
        }
    }
}
