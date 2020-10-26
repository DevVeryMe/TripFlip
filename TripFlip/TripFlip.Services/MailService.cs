using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using TripFlip.Services.Configurations;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services
{
    ///<inheritdoc cref="IMailService"/>
    public class MailService : IMailService
    {
        private readonly MailServiceConfiguration _mailServiceConfiguration;

        public MailService(MailServiceConfiguration mailServiceConfiguration)
        {
            _mailServiceConfiguration = mailServiceConfiguration;
        }

        public async Task SendAsync(EmailMessage emailMessage)
        {
            try
            {
                var sendGridClient = new SendGridClient(_mailServiceConfiguration.SendGridApiKey);

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
            catch (Exception exception)
            {
                throw new InvalidOperationException(ErrorConstants.CannotSendMailMessage, exception);
            }
        }

        public async Task SendBirthdayConglratulatoryEmailAsync(
            string email,
            string userName)
        {
            throw new NotImplementedException();
        }

        public async Task SendUserStatisticAsync(UserStatisticModel userStatistic)
        {
            throw new NotImplementedException();
        }
    }
}
