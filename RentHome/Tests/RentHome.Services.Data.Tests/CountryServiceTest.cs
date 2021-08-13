namespace RentHome.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using Xunit;

    public class CountryServiceTest
    {
        [Fact]
        public async Task AllCountriesShouldReturnCorrectCount()
        {
            var list = new List<Country>();
            var mockRepo = new Mock<IRepository<Country>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Country>())).Callback((Country country) => list.Add(country));

            var service = new CountryService(mockRepo.Object);

            var country = new Country
            {
                Name = "test",
            };

            list.Add(country);

            var result = service.AllCountriesAsync();

            Assert.Equal(1, list.Count);
        }
    }
}
