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
        public static void SetEmailFrom(this EmailMessage message, EmailAddress from)
        {
            message.From = from;
        }

        /// <summary>
        /// Sets EmailMessage To field with given value.
        /// </summary>
        /// <param name="message">EmailMessage instance.</param>
        /// <param name="to">Email message receiver.</param>
        public static void SetEmailTo(this EmailMessage message, EmailAddress to)
        {
            message.To = to;
        }

        /// <summary>
        /// Sets EmailMessage Subject field with given value.
        /// </summary>
        /// <param name="message">EmailMessage instance.</param>
        /// <param name="subject">Email message subject.</param>
        public static void SetEmailSubject(this EmailMessage message, string subject)
        {
            message.Subject = subject;
        }

        /// <summary>
        /// Sets EmailMessage HtmlContent field with given value.
        /// </summary>
        /// <param name="message">EmailMessage instance.</param>
        /// <param name="htmlContent">Email message html content.</param>
        public static void SetEmailHtmlContent(this EmailMessage message, string htmlContent)
        {
            message.HtmlContent = htmlContent;
        }

        /// <summary>
        /// Sets EmailMessage PlainTextContent field with given value.
        /// </summary>
        /// <param name="message">EmailMessage instance.</param>
        /// <param name="plainTextContent">Email message plain text content.</param>
        public static void SetEmailPlainTextContent(this EmailMessage message, string plainTextContent)
        {
            message.PlainTextContent = plainTextContent;
        }
    }
}
