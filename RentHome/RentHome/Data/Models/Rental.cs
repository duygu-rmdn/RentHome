namespace RentHome.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Rental
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }

        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; }
        public virtual Property Property { get; set; }

        public string TenantId { get; set; }

        [ForeignKey("TenantId")]
        public virtual User Tenant { get; set; }

        public int? Duration { get; set; }

        [ForeignKey(nameof(Contract))]
        public string ContractId { get; set; }
        public virtual Contract Contract { get; set; }
    }
}
