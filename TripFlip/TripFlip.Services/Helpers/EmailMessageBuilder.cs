using SendGrid.Helpers.Mail;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services.Helpers
{
    public static class EmailMessageBuilder
    {
        public static EmailMessage Build(
            EmailAddress from,
            EmailAddress to,
            string subject,
            string htmlContent,
            string plainTextContent)
        {
            var emailMessage = new EmailMessage()
            {
                From = from,
                To = to,
                Subject = subject,
                HtmlContent = htmlContent,
                PlainTextContent = plainTextContent
            };

            return emailMessage;
        }
    }
}
