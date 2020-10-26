using System.Threading.Tasks;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    /// <inheritdoc cref="IScheduledTaskService"/>
    public class ScheduledTaskService : IScheduledTaskService
    {
        readonly IMailService _mailService;

        readonly IUserService _userService;

        public ScheduledTaskService(
            IMailService mailService,
            IUserService userService)
        {
            _mailService = mailService;
            _userService = userService;
        }

        /// <summary>
        /// Gets a collection of users to congratulate on their birthday
        /// and uses <see cref="IMailService"/> to send congratulatory email
        /// to each of them.
        /// </summary>
        public async Task GreetBirthdayUsersAsync()
        {
            var birthdayUsers = await _userService.GetUsersWithBirthdayTodayAsync();

            foreach (var user in birthdayUsers)
            {
                await _mailService.SendBirthdayCongratulatoryEmailAsync(
                    user.Email, user.FirstName);
            }
        }
    }
}
