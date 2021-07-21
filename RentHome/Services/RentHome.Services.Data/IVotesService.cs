namespace RentHome.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(string propertyId, string userId, byte value);

        double GetAverageVote(string propertyId);
    }
}
