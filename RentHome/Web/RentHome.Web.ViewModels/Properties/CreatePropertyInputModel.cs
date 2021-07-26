namespace RentHome.Web.ViewModels.Properties
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.City;
    using RentHome.Web.ViewModels.Country;

    using static RentHome.Common.GlobalConstants;

    public class CreatePropertyInputModel : BasePropertyInputModel
    {
        [Required]
        [MinLength(PropertyAddressMin)]
        [MaxLength(PropertyAddressMax)]
        [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]

        public string Address { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        [Required]
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<CityListServiceModel> CityList { get; set; }

        public IEnumerable<CountryListServiceModel> CountryList { get; set; }
    }
}
