namespace RentHome.Services.Data
{
    using RentHome.Services.Messaging;
    using RentHome.Web.ViewModels.ContactUs;

    using static RentHome.Common.GlobalConstants;

    public class ContactService : IContactService
    {
        private readonly IEmailSenderService emailSender;

        public ContactService(IEmailSenderService emailSender)
        {
            this.emailSender = emailSender;
        }

        public void Contact(ContactInputModel input)
        {
            var from = input.YourEmail;
            var to = SystemEmail;
            var subject = input.Subject;
            var html = input.Message;

            this.emailSender.SendMail(from, to, subject, html);
        }

        public void ContactWithOwner(ContactInputModel input, string ownerEmail)
        {
            var from = input.YourEmail;
            var to = ownerEmail;
            var subject = input.Subject;
            var html = input.Message;

            this.emailSender.SendMail(from, to, subject, html);
        }
    }
}
