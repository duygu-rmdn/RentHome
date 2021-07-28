namespace RentHome.Web.ViewModels.Search
{
    using System.ComponentModel.DataAnnotations;

    using RentHome.Data.Models.Enums;

    using static RentHome.Common.GlobalConstants;

    public abstract class BaseSearchModel
    {
        [Range(typeof(decimal), PriceMin, PriceMax)]
        public decimal? MinPrice { get; set; }

        [Range(typeof(decimal), PriceMin, PriceMax)]
        public decimal? MaxPrice { get; set; }

        [EnumDataType(typeof(PropertyStatus))]
        public PropertyStatus Status { get; set; }

        [EnumDataType(typeof(PropertyCategory))]
        public PropertyCategory Category { get; set; }

        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }
    }
}
