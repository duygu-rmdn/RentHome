namespace RentHome.Web.ViewModels.Rental
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.Properties;

    using static RentHome.Common.GlobalConstants;

    public class RequestInputModel
    {
        [Required]
        [MinLength(MessageMinLenght)]
        [MaxLength(MessageMaxLenght)]
        public string Message { get; set; }

        [Range(DurationMinLenght, int.MaxValue)]
        [Display(Name = "Duration (nights)")]
        public int? Duration { get; set; }

        [Required]
        public DateTime? RentDate { get; set; }

        public PropertyStatus Status { get; set; }

        public PropertiesInListViewModel Property { get; set; }
    }
}
