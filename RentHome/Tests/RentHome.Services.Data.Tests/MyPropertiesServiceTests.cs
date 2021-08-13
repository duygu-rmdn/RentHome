namespace RentHome.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using Xunit;

    public class MyPropertiesServiceTests
    {
        [Fact]
        public void GetMyPropertiesShouldReturnCorrectCount()
        {
            var list = new List<Property>();
            var mockRepo = new Mock<IRepository<Property>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Property>())).Callback((Property property) => list.Add(property));

            var service = new MyPropertiesService(mockRepo.Object);

            var user = new ApplicationUser()
            {
                Id = "1",
                Properties = new List<Property>() { new Property { Name = "test" } },
                FirstName = "test",
                LastName = "test",
                UserName = "test",
            };

            Assert.Equal(1, user.Properties.Count);
        }
    }
}
