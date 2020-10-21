using SendGrid.Helpers.Mail;
using TripFlip.Services.Interfaces.Models;
using TripFlip.Services.Extensions;

namespace TripFlip.Services.Helpers
{
    /// <summary>
    /// Helper class for building EmailMessage.
    /// </summary>
    public static class EmailMessageBuilder
    {
        /// <summary>
        /// Sets EmailMessage fields with given parameters' values.
        /// </summary>
        /// <param name="from">Email message sender.</param>
        /// <param name="to">Email message receiver.</param>
        /// <param name="subject">Email message subject.</param>
        /// <param name="htmlContent">Email message html content.</param>
        /// <param name="plainTextContent">Email message plain text content.</param>
        /// <returns>An instance of EmailMessage.</returns>
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
