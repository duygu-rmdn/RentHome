namespace RentHome.Web.ViewModels.Properties
{
    using System.ComponentModel.DataAnnotations;

    using RentHome.Data.Models.Enums;

    using static RentHome.Common.GlobalConstants;

    public abstract class BasePropertyInputModel
    {
        [Required]
        [MinLength(PropertyNameMin)]
        [MaxLength(PropertyNameMax)]
        public string Name { get; set; }

        [Required]
        [MinLength(PropertyDescriptionMin)]
        [MaxLength(PropertyDescriptionMax)]

        public string Description { get; set; }

        [Range(typeof(decimal), PriceMin, PriceMax)]
        public decimal Price { get; set; }

        [Required]
        [EnumDataType(typeof(PropertyStatus))]
        public PropertyStatus Status { get; set; }

        [Required]
        [EnumDataType(typeof(PropertyCategory))]
        public PropertyCategory Category { get; set; }
    }
}
