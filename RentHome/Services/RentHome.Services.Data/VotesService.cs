namespace RentHome.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using RentHome.Data.Common.Repositories;
    using RentHome.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public async Task SetVoteAsync(string propertyId, string userId, byte value)
        {
            var vote = this.votesRepository.All().FirstOrDefault(x => x.PropertyId == propertyId && x.UserId == userId);
            if (vote == null)
            {
                vote = new Vote
                {
                    PropertyId = propertyId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }

        public double GetAverageVote(string propertyId)
        {
            var votes = this.votesRepository.All()
                .Where(x => x.PropertyId == propertyId);

            if (votes.Count() == 0)
            {
                return 0;
            }

            return votes.Average(x => x.Value);
        }
    }
}
