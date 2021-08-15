namespace RentHome.Web.ViewModels.Administration.Location
{
    using System.ComponentModel.DataAnnotations;

    using static RentHome.Common.GlobalConstants;

    public class LocationIndexFormModel
    {
        [Required]
        [MinLength(CityMinLenght)]
        [MaxLength(CityMaxLenght)]
        public string City { get; set; }

        [Required]
        [MinLength(CountryMinLenght)]
        [MaxLength(CountryMaxLenght)]
        public string Country { get; set; }

        [Required]
        [StringLength(CountryCodeLength)]
        public string CountryCode { get; set; }
    }
}
