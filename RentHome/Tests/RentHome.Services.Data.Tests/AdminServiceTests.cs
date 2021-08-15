namespace RentHome.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using RentHome.Web.Areas.Administration.Controllers;
    using RentHome.Web.Areas.Administration.Services;
    using RentHome.Web.ViewModels.Properties;
    using Xunit;

    public class AdminServiceTests
    {
        [Fact]
        public async Task ChangeVisibilityShouldWorkCorrect()
        {
            var list = new List<Property>();
            var mockRepo = new Mock<IRepository<Property>>();
            var cityMockRepo = new Mock<IRepository<City>>();
            var countryMockRepo = new Mock<IRepository<Country>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Property>())).Callback((Property property) => list.Add(property));

            var service = new AdminPropertyService(mockRepo.Object, cityMockRepo.Object, countryMockRepo.Object);

            var property = new Property
            {
                Id = "1",
                Name = "test",
                IsPublic = false,
            };
            list.Add(property);

            await service.ChangeVisility("1");

            Assert.True(property.IsPublic);
            Assert.Equal(1, list.Count());
        }
    }
}
