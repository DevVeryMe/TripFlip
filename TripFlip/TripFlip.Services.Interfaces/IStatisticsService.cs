using System;
using System.Threading.Tasks;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services.Interfaces
{
    public interface IStatisticsService
    {
        public Task<UserStatisticsModel> GetUserMonthStatisticsById(Guid userId);

        public Task<UserStatisticsModel> GetUserTotalStatisticsById(Guid userId);
    }
}
