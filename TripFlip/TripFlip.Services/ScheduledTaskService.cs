using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    /// <inheritdoc cref="IScheduledTaskService"/>
    public class ScheduledTaskService : IScheduledTaskService
    {
        readonly IMailService _mailService;

        readonly IUserService _userService;

        private readonly IStatisticService _statisticService;

        private readonly TripFlipDbContext _tripFlipDbContext;

        public ScheduledTaskService(
            IMailService mailService,
            IUserService userService,
            IStatisticService statisticService,
            TripFlipDbContext tripFlipDbContext)
        {
            _mailService = mailService;
            _userService = userService;
            _statisticService = statisticService;
            _tripFlipDbContext = tripFlipDbContext;
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
                await _mailService.SendBirthdayConglratulatoryEmailAsync(
                    user.Email, user.FirstName);
            }
        }

        /// <summary>
        /// Gets statistic data
        /// and uses <see cref="IMailService"/> to send it to appropriate user.
        /// </summary>
        public async Task SendUserStatisticAsync()
        {
            foreach (var user in _tripFlipDbContext.Users)
            {
                var userStatistic = await _statisticService.GetUserStatisticByIdAsync(user.Id);

                await _mailService.SendUserStatisticAsync(userStatistic);
            }
        }
    }
}
