using SendGrid.Helpers.Mail;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services.Extensions
{
    /// <summary>
    /// EmailMessage class extension methods.
    /// </summary>
    public static class EmailMessageExtensions
    {
        /// <summary>
        /// Sets EmailMessage From field with given value.
        /// </summary>
        /// <param name="message">EmailMessage instance.</param>
        /// <param name="from">Email message sender.</param>
        public static EmailMessage SetEmailFrom(this EmailMessage message, EmailAddress from)
        {
            message.From = from;

            return message;
        }

        /// <summary>
        /// Sets EmailMessage To field with given value.
        /// </summary>
        /// <param name="message">EmailMessage instance.</param>
        /// <param name="to">Email message receiver.</param>
        public static EmailMessage SetEmailTo(this EmailMessage message, EmailAddress to)
        {
            message.To = to;

            return message;
        }

        /// <summary>
        /// Sets EmailMessage Subject field with given value.
        /// </summary>
        /// <param name="message">EmailMessage instance.</param>
        /// <param name="subject">Email message subject.</param>
        public static EmailMessage SetEmailSubject(this EmailMessage message, string subject)
        {
            message.Subject = subject;

            return message;
        }

        /// <summary>
        /// Sets EmailMessage HtmlContent field with given value.
        /// </summary>
        /// <param name="message">EmailMessage instance.</param>
        /// <param name="htmlContent">Email message html content.</param>
        public static EmailMessage SetEmailHtmlContent(this EmailMessage message, string htmlContent)
        {
            message.HtmlContent = htmlContent;

            return message;
        }

        /// <summary>
        /// Sets EmailMessage PlainTextContent field with given value.
        /// </summary>
        /// <param name="message">EmailMessage instance.</param>
        /// <param name="plainTextContent">Email message plain text content.</param>
        public static EmailMessage SetEmailPlainTextContent(this EmailMessage message, string plainTextContent)
        {
            message.PlainTextContent = plainTextContent;

            return message;
        }
    }
}
