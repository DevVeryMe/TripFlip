using System;

namespace TripFlip.Services.Dto.UserDtos
{
    public class GrantSubscriberRoleDto
    {
        public int TripId { get; set; }

        public Guid UserId { get; set; }

        public int TripRoleId { get; set; }
    }
}
