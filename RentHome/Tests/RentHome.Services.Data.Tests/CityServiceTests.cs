namespace RentHome.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using Xunit;

    public class CityServiceTests
    {
        [Fact]
        public async Task AllCitiesShouldReturnCorrectCount()
        {
            var list = new List<City>();
            var mockRepo = new Mock<IRepository<City>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<City>())).Callback((City city) => list.Add(city));

            var service = new CityService(mockRepo.Object);

            var city = new City
            {
                Name = "test",
            };
            var city1 = new City
            {
                Name = "test1",
            };
            list.Add(city);
            list.Add(city1);

            var result = service.AllCitiesAsync();

            Assert.Equal(2, list.Count);
        }
    }
}
