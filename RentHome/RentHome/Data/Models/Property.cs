namespace RentHome.Data.Models
{
    using RentHome.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.GlobalConstants;

    public class Property
    {
        public Property()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Images = new HashSet<CloudImage>();
        }

        public string Id { get; set; }

        [Required]
        [MinLength(HomeNameMin)]
        [MaxLength(HomeNameMax)]
        public string Name { get; set; }

        [Required]
        [MinLength(HomeDescriptionMin)]
        [MaxLength(HomeDescriptionMax)]
        public string Description { get; set; }

        [Required]
        [MinLength(HomeAddressMin)]
        [MaxLength(HomeAddressMax)]
        [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]
        public string Address { get; set; }

        [Range(typeof(decimal), PriceMin, PriceMax)]
        public decimal Price { get; set; }

        [Required]
        public HomeStatus Status { get; set; }

        [Required]
        public HomeCategory Category { get; set; }

        public string OwnerId { get; set; }

        [Required]
        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        public string ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public virtual User Manager { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public ICollection<CloudImage> Images { get; set; }

    }
}
