using System;
using System.Collections.Generic;
using TripFlip.Domain.Entities.Enums;

namespace TripFlip.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AboutMe { get; set; }

        public UserGender? Gender { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        public ICollection<TripSubscriberEntity> TripSubscriptions { get; set; }

        public ICollection<ApplicationUserRoleEntity> ApplicationRoles { get; set; }

    }
}
