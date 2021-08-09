namespace RentHome.Web.ViewModels.Rental
{
    using System.ComponentModel.DataAnnotations;

    using RentHome.Web.ViewModels.Properties;

    using static RentHome.Common.GlobalConstants;

    public class RequestInputModel
    {
        [Required]
        [MinLength(MessageMinLenght)]
        [MaxLength(MessageMaxLenght)]
        public string Message { get; set; }

        public PropertiesInListViewModel Property { get; set; }
    }
}
