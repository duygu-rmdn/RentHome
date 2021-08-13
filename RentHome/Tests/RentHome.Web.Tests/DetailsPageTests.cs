namespace RentHome.Web.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class DetailsPageTests
    {
        [Fact]
        public async Task ContactUsPageShouldReturnSuccessStatus()
        {
            var wepApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient client = wepApplicationFactory.CreateClient();

            var response = await client.GetAsync("/Properties/Details/763f1a20-e549-48f9-a7a9-e03fdbba40d4");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ContactUsPageShouldContaionListingsHeading()
        {
            var wepApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient client = wepApplicationFactory.CreateClient();

            var response = await client.GetAsync("/Properties/Details/763f1a20-e549-48f9-a7a9-e03fdbba40d4");

            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("<title>Details - RentHome</title>", html);
        }
    }
}
