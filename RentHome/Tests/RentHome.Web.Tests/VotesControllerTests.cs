namespace RentHome.Web.Tests
{
    using MyTested.AspNetCore.Mvc;
    using RentHome.Web.Controllers;
    using Xunit;

    using static RentHome.Web.Tests.Data.VoteData;

    public class VotesControllerTests
    {
        [Fact]
        public void PostMethodShouldWorkCorrect()
            => MyController<VotesController>
            .Instance(
                contoller => contoller.WithData(Vote()).WithUser("1"))
            .Calling(x => x.Post(PostVoteInputModel()))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
    }
}
