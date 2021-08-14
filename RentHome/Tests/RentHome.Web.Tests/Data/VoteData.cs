namespace RentHome.Web.Tests.Data
{
    using RentHome.Data.Models;
    using RentHome.Web.ViewModels.Votes;

    public class VoteData
    {
        public static Vote Vote()
            => new Vote
            {
                Id = 1,
                PropertyId = "1",
                UserId = "1",
                Value = 3,
            };

        public static PostVoteInputModel PostVoteInputModel()
            => new PostVoteInputModel
            {
                PropertyId = "1",
                Value = 5,
            };

    }
}
