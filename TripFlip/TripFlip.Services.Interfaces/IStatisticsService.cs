using System;
using System.Threading.Tasks;
using TripFlip.Services.Dto.UserStatisticsDtos;

namespace TripFlip.Services.Interfaces
{
    public interface IStatisticsService
    {
        public Task<UserStatisticsDto> GetUserStatisticsById(Guid userId);
    }
}
