﻿using System;

namespace GoogleAuthentication.ViewModels
{
    public class AuthenticatedUserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}