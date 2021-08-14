namespace RentHome.Web.Tests.Data
{
    using RentHome.Web.ViewModels.ContactUs;

    public class ContactUsData
    {
        public static ContactInputModel ContactData()
            => new ContactInputModel
            {
                YourName = "Namdfdfdfdfe",
                YourEmail = "jsdnsjdnsjds@gmail.com",
                Subject = "testSubject",
                Message = "SomeMesageHeSomeMesageHereSomeMesageHerere",
            };
    }
}
