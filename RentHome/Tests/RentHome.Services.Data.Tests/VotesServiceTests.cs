namespace RentHome.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task WhenUserVotes2TimesOnly1ShouldByCounted()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));
            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync("1", "2", 1);
            await service.SetVoteAsync("1", "2", 5);
            await service.SetVoteAsync("1", "2", 5);
            await service.SetVoteAsync("1", "2", 5);
            await service.SetVoteAsync("1", "2", 5);

            Assert.Equal(1, list.Count);
            Assert.Equal(5, list.First().Value);
        }

        [Fact]
        public async Task When2UsersVoteFor1PropertyAverageVoteShouldBeCorrect()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));
            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync("1", "1", 1);
            await service.SetVoteAsync("1", "2", 5);
            await service.SetVoteAsync("1", "2", 2);

            Assert.Equal(2, list.Count);
            Assert.Equal(1.5, service.GetAverageVote("1"));
        }

        [Fact]
        public async Task WhenVotestCountAre0AverageVoteShouldBe0()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));
            var service = new VotesService(mockRepo.Object);

            Assert.Equal(0, list.Count);
        }
    }
}
