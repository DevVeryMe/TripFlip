using TripFlip.Services.Dto.UserDtos;

namespace TripFlip.Services.Interfaces.Models
{
    public class UserStatisticModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public int RoutesWhereHasAdminRoleCount { get; set; }

        public int RoutesWhereNotAdminCount { get; set; }

        public int CompletedTasksCount { get; set; }

        public int CompletedItemsCount { get; set; }
    }
}
