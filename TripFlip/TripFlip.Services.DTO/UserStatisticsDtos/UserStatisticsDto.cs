using TripFlip.Services.Dto.UserDtos;

namespace TripFlip.Services.Dto.UserStatisticsDtos
{
    public class UserStatisticsDto
    {
        public UserDto User { get; set; }

        public int RoutesCountWhereHasAdminRole { get; set; }

        public int RoutesCountWhereNotAdmin { get; set; }

        public int CompletedTasksCount { get; set; }

        public int CompletedItemsCount { get; set; }
    }
}
