namespace RentHome.Services.Data
{
    using System.Threading.Tasks;

    using RentHome.Web.ViewModels.ContactUs;

    public interface IContactService
    {
        Task Contact(ContactInputModel input);

        Task ContactWithOwner(ContactInputModel input, string ownerEmail);
    }
}
