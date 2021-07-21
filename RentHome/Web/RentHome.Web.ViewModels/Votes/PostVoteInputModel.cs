namespace RentHome.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class PostVoteInputModel
    {
        public string PropertyId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
