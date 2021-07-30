namespace RentHome.Services.Data
{
    using System.Threading.Tasks;

    using RentHome.Services.Messaging;
    using RentHome.Web.ViewModels.ContactUs;

    using static RentHome.Common.GlobalConstants;

    public class ContactService : IContactService
    {
        private readonly IEmailSender emailSender;

        public ContactService(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public async Task Contact(ContactInputModel input)
        {
            var from = input.YourEmail;
            var fromName = input.YourName;
            var to = SystemEmail;
            var subject = input.Subject;
            var html = input.Message;

            await this.emailSender.SendEmailAsync(from, fromName, to, subject, html);
        }

        public async Task ContactWithOwner(ContactInputModel input, string ownerEmail)
        {
            var from = input.YourEmail;
            var fromName = input.YourName;
            var to = ownerEmail;
            var subject = input.Subject;
            var html = input.Message;

            await this.emailSender.SendEmailAsync(from, fromName, to, subject, html);
        }
    }
}
