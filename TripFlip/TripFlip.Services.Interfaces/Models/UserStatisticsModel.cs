using TripFlip.Services.Dto.UserDtos;

namespace TripFlip.Services.Interfaces.Models
{
    public class UserStatisticsModel
    {
        public string Email { get; set; }

        public int RoutesCountWhereHasAdminRole { get; set; }

        public int RoutesCountWhereNotAdmin { get; set; }

        public int CompletedTasksCount { get; set; }

        public int CompletedItemsCount { get; set; }
    }
}
