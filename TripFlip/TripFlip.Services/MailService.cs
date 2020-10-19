using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using TripFlip.Services.Configurations;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services
{
    public class MailService : IMailService
    {
        private readonly SendGridConfiguration _sendGridConfiguration;

        public MailService(SendGridConfiguration sendGridConfiguration)
        {
            _sendGridConfiguration = sendGridConfiguration;
        }

        public async Task SendAsync(EmailMessage emailMessage)
        {
            var sendGridClient = new SendGridClient(_sendGridConfiguration.ApiKey);

            var message = new SendGridMessage()
            {
                From = emailMessage.From,
                Subject = emailMessage.Subject,
                PlainTextContent = emailMessage.PlainTextContent,
                HtmlContent = emailMessage.HtmlContent
            };

            message.AddTo(emailMessage.To);

            await sendGridClient.SendEmailAsync(message);
        }
    }
}
