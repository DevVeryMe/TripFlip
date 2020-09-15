using System;
using TripFlip.ViewModels.Enums;

namespace TripFlip.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AboutMe { get; set; }

        public UserGender? Gender { get; set; }

        public DateTimeOffset? BirthDate { get; set; }
    }
}
