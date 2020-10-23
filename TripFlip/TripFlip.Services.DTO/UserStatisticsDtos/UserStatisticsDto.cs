using TripFlip.Services.Dto.UserDtos;

namespace TripFlip.Services.Dto.UserStatisticsDtos
{
    public class UserStatisticsDto
    {
        public UserDto User { get; set; }

        public int LastMonthRoutesCountWhereHasAdminRole { get; set; }

        public int TotalRoutesCountWhereHasAdminRole { get; set; }

        public int LastMonthRoutesCountWhereNotAdmin { get; set; }

        public int TotalRoutesCountWhereNotAdmin { get; set; }

        public int LastMonthCompletedTasksCount { get; set; }

        public int TotalCompletedTasksCount { get; set; }

        public int LastMonthCompletedItemsCount { get; set; }

        public int TotalCompletedItemsCount { get; set; }
    }
}
