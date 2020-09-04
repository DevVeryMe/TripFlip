using System;

namespace TripFlip.Services.Dto.UserDtos
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
    }
}
