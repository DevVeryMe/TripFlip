using System;
using System.Threading.Tasks;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Service that provides methods for getting statistic data.
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Gets user statistic data for the last month.
        /// </summary>
        /// <param name="userId">Id of user to get statistic data for.</param>
        /// <returns>UserStatisticModel instance, which represents user statistic data.</returns>
        public Task<UserStatisticModel> GetUserStatisticById(Guid userId);
    }
}
