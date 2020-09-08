using System;
using System.Collections.Generic;

namespace TripFlip.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public ICollection<TripSubscriberEntity> TripSubscriptions { get; set; }

        public ICollection<ApplicationUserRoleEntity> ApplicationRoles { get; set; }

    }
}
