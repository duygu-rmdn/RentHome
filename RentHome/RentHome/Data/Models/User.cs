namespace RentHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.GlobalConstants;

    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Properties = new HashSet<Property>();
            this.Rentals = new HashSet<Rental>();
            this.ManagedProperties = new HashSet<Property>();
            this.Contracts = new HashSet<Contract>();
        }

        public string Id { get; set; }

        [Required]
        [MinLength(UsersNameMin)]
        [MaxLength(UsersNameMax)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(UsersNameMin)]
        [MaxLength(UsersNameMax)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        public CloudImage ProfilePic { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        [MaxLength(AboutMaxLength)]
        public string About { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }

        public virtual ICollection<Property> ManagedProperties { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
