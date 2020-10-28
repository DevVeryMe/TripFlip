using Microsoft.AspNetCore.Hosting;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Threading.Tasks;
using TripFlip.Services.Configurations;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services
{
    ///<inheritdoc cref="IMailService"/>
    public class MailService : IMailService
    {
        private readonly MailServiceConfiguration _mailServiceConfiguration;

        private readonly IWebHostEnvironment _environment;

        public MailService(MailServiceConfiguration mailServiceConfiguration,
            IWebHostEnvironment environment)
        {
            _mailServiceConfiguration = mailServiceConfiguration;
            _environment = environment;
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

        public async Task SendBirthdayCongratulatoryEmailAsync(
            string email,
            string congratulationMessage)
        {
            var appEmailAddress = new EmailAddress(
                _mailServiceConfiguration.AppFromEmail, _mailServiceConfiguration.AppFromName);
            var userEmailAddress = new EmailAddress(email);

            var emailMessage = EmailMessageBuilder.Build(
                from: appEmailAddress,
                to: userEmailAddress,
                subject: Constants.BirthdayCongratulationEmailSubject,
                htmlContent: congratulationMessage,
                plainTextContent: null);

            await SendAsync(emailMessage);
        }

        public async Task SendUserStatisticAsync(string email, string userStatisticMessage)
        {
            var appEmailAddress = new EmailAddress(
                    _mailServiceConfiguration.AppFromEmail, _mailServiceConfiguration.AppFromName);
            var userEmailAddress = new EmailAddress(email);

            var emailMessage = EmailMessageBuilder.Build(
                    from: appEmailAddress,
                    to: userEmailAddress,
                    subject: Constants.UserStatisticEmailSubject,
                    htmlContent: userStatisticMessage,
                    plainTextContent: null);

            await SendAsync(emailMessage);
        }

        public string BuildUserStatisticString(
            UserStatisticModel userStatistic,
            string statisticTemplate)
        {
            return string.Format(
                statisticTemplate,
                userStatistic.FirstName ?? userStatistic.Email,
                userStatistic.LastMonthTripsWhereHasAdminRoleCount,
                userStatistic.TotalTripsWhereHasAdminRoleCount,
                userStatistic.LastMonthUsersInHisRoutesCount,
                userStatistic.TotalUsersInHisRoutesCount,
                userStatistic.LastMonthRoutesWhereHasAdminRoleCount,
                userStatistic.TotalRoutesWhereHasAdminRoleCount,
                userStatistic.LastMonthRoutesWhereNotAdminCount,
                userStatistic.TotalRoutesWhereNotAdminCount,
                userStatistic.LastMonthCompletedTasksCount,
                userStatistic.TotalCompletedTasksCount,
                userStatistic.LastMonthCompletedItemsCount,
                userStatistic.TotalCompletedItemsCount);
        }

        public string BuildBirthdayCongratulationString(string userName, string congratulationTemplate)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = Constants.TripFlipUser;
            }

            return string.Format(congratulationTemplate, userName);
        }

        /// <summary>
        /// Reads user statistic template file and returns it's
        /// contents as string.
        /// </summary>
        /// <returns>User statistic template string.</returns>
        public async Task<string> GetUserStatisticTemplateAsync()
        {
            string userStatsMessageFilepath = Path.Combine(
                _environment.WebRootPath, _mailServiceConfiguration.UserStatisticMessageFilename);

            bool fileExists = File.Exists(userStatsMessageFilepath);

            string template = default;

            if (fileExists)
            {
                template = await File.ReadAllTextAsync(userStatsMessageFilepath);
            }

            return template;
        }

        /// <summary>
        /// Reads birthday congratulation template file and returns it's
        /// contents as string.
        /// </summary>
        /// <returns>Birthday congratulation template string.</returns>
        public async Task<string> GetBirthdayCongratulationTemplateAsync()
        {
            var congratulationFilepath = Path.Combine(
                _environment.WebRootPath, _mailServiceConfiguration.BirthdayCongratulationFilename);

            var fileExists = File.Exists(congratulationFilepath);

            if (!fileExists)
            {
                throw new NotFoundException(ErrorConstants.BirthdayCongratulationTemplateFileNotFound);
            }

            var congratulationHtmlTemplate = await File.ReadAllTextAsync(congratulationFilepath);

            return congratulationHtmlTemplate;
        }
    }
}
