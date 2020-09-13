using System.Collections.Generic;

namespace TripFlip.ViewModels.UserViewModels
{
    public class UsersByTripAndCategorizedByRoleViewModel
    {
        public int TripId { get; set; }

        public IEnumerable<UserViewModel> TripAdmins { get; set; }

        public IEnumerable<UserViewModel> TripEditors { get; set; }

        public IEnumerable<UserViewModel> TripGuests { get; set; }
    }
}
