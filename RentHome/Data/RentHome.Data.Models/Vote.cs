namespace RentHome.Data.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public byte Value { get; set; }

        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
