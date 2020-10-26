using System;
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
        /// <param name="userName">User's name.</param>
        Task SendBirthdayConglratulatoryEmailAsync(string email, string userName);

        /// <summary>
        /// Sends email letter with statistic data to appropriate user.
        /// </summary>
        /// <param name="userStatistic">UserStatisticModel instance,
        /// which carries all needed data to send email.</param>
        Task SendUserStatisticAsync(UserStatisticModel userStatistic);
    }
}
