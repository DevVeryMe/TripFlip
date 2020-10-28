using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                await _mailService.SendBirthdayCongratulatoryEmailAsync(
                    user.Email, user.FirstName);
            }
        }

        /// <summary>
        /// Gets statistic data and uses <see cref="IMailService"/> 
        /// to send it to a corresponding user.
        /// </summary>
        public async Task SendUserStatisticAsync()
        {
            string statisticEmailTemplate = 
                await _mailService.GetUserStatisticTemplateAsync();

            if (!string.IsNullOrWhiteSpace(statisticEmailTemplate))
            {
                var users = await _tripFlipDbContext.Users.AsNoTracking().ToListAsync();

                foreach (var user in users)
                {
                    var userStatistic = await _statisticService.GetUserStatisticByIdAsync(user.Id);

                    string userStatisticString = 
                        _mailService.BuildUserStatisticString(userStatistic, statisticEmailTemplate);

                    await _mailService.SendUserStatisticAsync(userStatistic.Email, userStatisticString);
                }
            }
        }
    }
}
