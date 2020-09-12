using System;
using TripFlip.Services.Dto.Enums;

namespace TripFlip.Services.Dto.UserDtos
{
    public class GrantApplicationRoleDto
    {
        public Guid UserId { get; set; }

        public ApplicationRole ApplicationRole { get; set; }
    }
}
