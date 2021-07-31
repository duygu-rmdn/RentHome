namespace RentHome.Services.Data
{
    using RentHome.Web.ViewModels.ContactUs;

    public interface IContactService
    {
        void Contact(ContactInputModel input);

        void ContactWithOwner(ContactInputModel input, string ownerEmail);
    }
}
