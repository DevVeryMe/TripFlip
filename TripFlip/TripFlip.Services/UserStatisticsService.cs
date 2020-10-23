using System;
using System.Threading.Tasks;
using TripFlip.Services.Dto.UserStatisticsDtos;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    public class StatisticsService : IStatisticsService
    {
        public async Task<UserStatisticsDto> GetUserMonthStatisticsById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserStatisticsDto> GetUserTotalStatisticsById(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
