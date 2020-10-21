using SendGrid.Helpers.Mail;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services.Extensions
{
    public static class EmailMessageExtensions
    {
        public static void SetEmailFrom(this EmailMessage message, EmailAddress from)
        {
            message.From = from;
        }

        public static void SetEmailTo(this EmailMessage message, EmailAddress to)
        {
            message.To = to;
        }

        public static void SetEmailSubject(this EmailMessage message, string subject)
        {
            message.Subject = subject;
        }

        public static void SetEmailHtmlContent(this EmailMessage message, string htmlContent)
        {
            message.HtmlContent = htmlContent;
        }

        public static void SetEmailPlainTextContent(this EmailMessage message, string plainTextContent)
        {
            message.PlainTextContent = plainTextContent;
        }
    }
}
