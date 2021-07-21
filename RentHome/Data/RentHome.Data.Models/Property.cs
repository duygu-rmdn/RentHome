namespace RentHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using RentHome.Data.Models.Enums;

    using static RentHome.Common.GlobalConstants;

    public class Property
    {
        public Property()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Images = new HashSet<Image>();
            this.Votes = new HashSet<Vote>();
        }

        public string Id { get; set; }

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
        public PropertyStatus Status { get; set; }

        [Required]
        public PropertyCategory Category { get; set; }

        public string OwnerId { get; set; }

        [Required]
        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }

        public string ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public virtual ApplicationUser Manager { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<Image> Images { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
