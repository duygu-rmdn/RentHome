namespace RentHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using static RentHome.Common.GlobalConstants;

    public class Contract
    {
        public Contract()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(ContractTitleMaxLength, ErrorMessage = TitleTooLong)]
        public string Title { get; set; }

        public int? RentalId { get; set; }

        [ForeignKey("RentalId")]
        public virtual Rental Rental { get; set; }

        public string ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public virtual ApplicationUser Manager { get; set; }
    }
}
