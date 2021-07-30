namespace RentHome.Web.ViewModels.ContactUs
{
    using System.ComponentModel.DataAnnotations;

    using static RentHome.Common.GlobalConstants;

    public class ContactInputModel
    {
        [Required]
        [MinLength(UsersNameMin)]
        [MaxLength(UsersNameMax)]
        [Display(Name = "Your Name")]
        public string YourName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Your Email")]
        public string YourEmail { get; set; }

        [Required]
        [MinLength(SubjectMinLenght)]
        [MaxLength(SubjectMaxLenght)]
        public string Subject { get; set; }

        [Required]
        [MinLength(MessageMinLenght)]
        [MaxLength(MessageMaxLenght)]
        public string Message { get; set; }
    }
}
