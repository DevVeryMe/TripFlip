using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using SendGrid.Helpers.Mail;
using TripFlip.Services.Configurations;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services.Helpers
{
    /// <summary>
    /// Helper class that provides methods to notify user with email.
    /// </summary>
    public static class EmailUserNotifierHelper
    {
        /// <summary>
        /// Sends an email to the newly registered user notifying him
        /// about successful registration act.
        /// </summary>
        /// <param name="email">An email of a user to send notification to.</param>
        /// <param name="environment">Instance of web host environment.</param>
        /// <param name="mailService">Instance of mail service.</param>
        /// <param name="mailServiceConfiguration">Object that encapsulates configurations for
        /// mail service.</param>
        public static async Task NotifyRegisteredUserAsync(
            string email,
            IWebHostEnvironment environment,
            IMailService mailService,
            MailServiceConfiguration mailServiceConfiguration)
        {
            string notificationFilepath = Path.Combine(
                environment.WebRootPath, mailServiceConfiguration.RegisteredUserNotificationFilename);

            bool fileExists = File.Exists(notificationFilepath);

            if (fileExists)
            {
                string notificationHtmlText = await File.ReadAllTextAsync(notificationFilepath);

                var appEmailAddress = new EmailAddress(
                    mailServiceConfiguration.AppFromEmail, mailServiceConfiguration.AppFromName);
                var userEmailAddress = new EmailAddress(email);

                var emailMessage = EmailMessageBuilder.Build(
                    from: appEmailAddress,
                    to: userEmailAddress,
                    subject: Constants.OnRegistrationNotificationEmailSubject,
                    htmlContent: notificationHtmlText,
                    plainTextContent: null);

                await mailService.SendAsync(emailMessage);
            }
        }
    }
}
