﻿using System;

namespace TripFlip.Services.Dto.UserDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTimeOffset DateCreated { get; set; }
    }
}
