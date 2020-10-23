using System;
using System.Threading.Tasks;
using TripFlip.Services.Dto.UserStatisticsDtos;

namespace TripFlip.Services.Interfaces
{
    public interface IStatisticsService
    {
        public Task<UserStatisticsDto> GetUserMonthStatisticsById(Guid userId);

        public Task<UserStatisticsDto> GetUserTotalStatisticsById(Guid userId);
    }
}
