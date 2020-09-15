using System;
using TripFlip.Services.Dto.Enums;

namespace TripFlip.Services.Dto.UserDtos
{
    public class RegisterUserDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AboutMe { get; set; }

        public UserGender? Gender { get; set; }

        public DateTimeOffset? BirthDate { get; set; }
    }
}
