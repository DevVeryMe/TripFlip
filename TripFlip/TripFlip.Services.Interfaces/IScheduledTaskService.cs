using System.Threading.Tasks;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Service that provides methods for scheduled tasks.
    /// </summary>
    public interface IScheduledTaskService
    {
        /// <summary>
        /// Method that represents a task of greeting birthday users.
        /// </summary>
        Task GreetBirthdayUsersAsync();
    }
}
