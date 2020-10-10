using System;

namespace GoogleAuthentication.Services.Dtos
{
    public class AuthenticatedUserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
