namespace RentHome.Web.ViewModels.Properties
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.City;
    using RentHome.Web.ViewModels.Country;

    using static RentHome.Common.GlobalConstants;

    public class CreatePropertyInputModel
    {
        [Required]
        [MinLength(PropertyNameMin)]
        [MaxLength(PropertyNameMax)]
        public string Name { get; set; }

        [Required]
        [MinLength(PropertyDescriptionMin)]
        [MaxLength(PropertyDescriptionMax)]

        public string Description { get; set; }

        [Required]
        [MinLength(PropertyAddressMin)]
        [MaxLength(PropertyAddressMax)]
        [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]

        public string Address { get; set; }

        [Range(typeof(decimal), PriceMin, PriceMax)]
        public decimal Price { get; set; }

        [Required]
        [EnumDataType(typeof(PropertyStatus))]
        public PropertyStatus Status { get; set; }

        [Required]
        [EnumDataType(typeof(PropertyCategory))]
        public PropertyCategory Category { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<CityListServiceModel> CityList { get; set; }

        public IEnumerable<CountryListServiceModel> CountryList { get; set; }
    }
}
