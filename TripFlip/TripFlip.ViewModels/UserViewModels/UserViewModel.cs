using System;

namespace TripFlip.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public DateTimeOffset DateCreated { get; set; }
    }
}
