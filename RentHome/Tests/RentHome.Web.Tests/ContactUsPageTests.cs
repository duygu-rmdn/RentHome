namespace RentHome.Web.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class ContactUsPageTests
    {
        [Fact]
        public async Task ContactUsPageShouldReturnSuccessStatus()
        {
            var wepApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient client = wepApplicationFactory.CreateClient();

            var response = await client.GetAsync("/ContactUs");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ContactUsPageShouldContaionListingsHeading()
        {
            var wepApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient client = wepApplicationFactory.CreateClient();

            var response = await client.GetAsync("/ContactUs");

            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("<h6>Contact info</h6>", html);
        }
    }
}
