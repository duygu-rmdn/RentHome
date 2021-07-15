namespace RentHome.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        public string Extention { get; set; }
    }
}
