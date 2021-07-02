namespace RentHome.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CloudImage
    {
        [Key]
        public int Id { get; set; }

        public string PicturePublicId { get; set; }

        public string PictureUrl { get; set; }
    }
}
