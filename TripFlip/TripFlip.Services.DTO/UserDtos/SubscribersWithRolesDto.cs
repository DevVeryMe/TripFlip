using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.TripRoleDtos;

namespace TripFlip.Services.Dto.UserDtos
{
    public class SubscriberWithRolesDto
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public ICollection<TripRoleDto> Roles { get; set; }
    }
}