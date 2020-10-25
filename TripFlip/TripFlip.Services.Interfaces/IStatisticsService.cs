using System;
using System.Threading.Tasks;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services.Interfaces
{
    public interface IStatisticsService
    {
        /// <summary>
        /// Gets user statistics data for the last month.
        /// </summary>
        /// <param name="userId">Id of user to get statistics data for.</param>
        /// <returns>UserStatisticsModel instance, which represents user statistics data.</returns>
        public Task<UserStatisticsModel> GetUserMonthStatisticsById(Guid userId);

        /// <summary>
        /// Gets user statistics data for all time.
        /// </summary>
        /// <param name="userId">Id of user to get statistics data for.</param>
        /// <returns>UserStatisticsModel instance, which represents user statistics data.</returns>
        public Task<UserStatisticsModel> GetUserTotalStatisticsById(Guid userId);
    }
}
