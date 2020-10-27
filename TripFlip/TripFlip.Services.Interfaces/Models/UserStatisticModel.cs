namespace TripFlip.Services.Interfaces.Models
{
    public class UserStatisticModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public int TotalTripsWhereHasAdminRoleCount { get; set; }

        public int TotalUsersInHisRoutesCount { get; set; }

        public int TotalRoutesWhereHasAdminRoleCount { get; set; }

        public int TotalRoutesWhereNotAdminCount { get; set; }

        public int TotalCompletedTasksCount { get; set; }

        public int TotalCompletedItemsCount { get; set; }

        public int LastMonthTripsWhereHasAdminRoleCount { get; set; }

        public int LastMonthUsersInHisRoutesCount { get; set; }

        public int LastMonthRoutesWhereHasAdminRoleCount { get; set; }

        public int LastMonthRoutesWhereNotAdminCount { get; set; }

        public int LastMonthCompletedTasksCount { get; set; }

        public int LastMonthCompletedItemsCount { get; set; }
    }
}
