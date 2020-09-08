using System;
using System.Collections.Generic;
using TripFlip.ViewModels.TripRoleViewModels;

namespace TripFlip.ViewModels.UserViewModels
{
    public class SubscriberWithRolesViewModel
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public ICollection<TripRoleViewModel> Roles { get; set; }
    }
}
