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
    }
}
