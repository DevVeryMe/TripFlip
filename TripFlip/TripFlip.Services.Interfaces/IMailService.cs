using System.Threading.Tasks;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Service that provides methods for interacting with users via emails.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Sends email message to user.
        /// </summary>
        /// <param name="emailMessage">Object that encapsulates all 
        /// email message properties like: subject, text content, etc</param>
        Task SendAsync(EmailMessage emailMessage);

        /// <summary>
        /// Sends congratulatory email letter to a user with a given email address.
        /// </summary>
        /// <param name="email">User's email address to send letter to.</param>
        /// <param name="congratulationMessage">Birthday congratulation message.</param>
        Task SendBirthdayCongratulatoryEmailAsync(string email, string congratulationMessage);

        /// <summary>
        /// Sends a given statistic message to a user with a given
        /// email address.
        /// </summary>
        /// <param name="email">User's email address to send letter to.</param>
        /// <param name="userStatisticMessage">A string that represents
        /// a complete message that contains user's statistic.</param>
        Task SendUserStatisticAsync(string email, string userStatisticMessage);

        /// <summary>
        /// Builds user statistic by inserting given <see cref="UserStatisticModel"/>
        /// object (that encapsulates user's statistic) into given statistic template.
        /// </summary>
        /// <param name="userStatistic">Object that encapsulates user's statistic.</param>
        /// <param name="statisticTemplate">User statistic template.</param>
        /// <returns>A user-friendly string view of user's statistic.</returns>
        string BuildUserStatisticString(
            UserStatisticModel userStatistic,
            string statisticTemplate);

        /// <summary>
        /// Gets user statistic template to put statistic data into.
        /// </summary>
        /// <returns>A string that represents a template
        /// to put user statistic data into.</returns>
        Task<string> GetUserStatisticTemplateAsync();

        /// <summary>
        /// Builds birthday congratulation by inserting given username
        /// into given congratulation template.
        /// </summary>
        /// <param name="userName">Name of user to congratulate.</param>
        /// <param name="congratulationTemplate">Congratulation template.</param>
        /// <returns>A user-friendly congratulation string view.</returns>
        string BuildBirthdayCongratulationString(
            string userName,
            string congratulationTemplate);

        /// <summary>
        /// Gets birthday congratulation template to put username data into.
        /// </summary>
        /// <returns>A string that represents a template
        /// to put username into.</returns>
        Task<string> GetBirthdayCongratulationTemplateAsync();
    }
}
