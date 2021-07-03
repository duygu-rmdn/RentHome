namespace RentHome.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using RentHome.Data.Models.Enums;

    using static RentHome.Common.GlobalConstants;

    public class Request
    {
        public Request()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public RequestType Type { get; set; }

        public string Message { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; }

        public Property Property { get; set; }

        [Required]
        [MaxLength(RequestDocumentMaxSize)]
        public byte[] Document { get; set; }

        public RequestStatus Status { get; set; }
    }
}
