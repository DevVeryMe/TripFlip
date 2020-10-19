using SendGrid.Helpers.Mail;

namespace TripFlip.Services.Interfaces.Models
{
    public class EmailMessage
    {
        public EmailAddress From { get; set; }

        public EmailAddress To { get; set; }

        public string Subject { get; set; }

        public string HtmlContent { get; set; }

        public string PlainTextContent { get; set; }
    }
}
