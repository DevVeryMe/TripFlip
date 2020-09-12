using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    /// <summary>
    /// Class that provides access to properties of the current user.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public Guid UserId
        {
            get
            {
                var userIdToParse = _httpContextAccessor
                    .HttpContext
                    .User
                    ?.Claims
                    ?.SingleOrDefault(c =>
                        c.Type == ClaimTypes.NameIdentifier)
                    ?.Value;

                if (!Guid.TryParse(userIdToParse, out var parsedUserId))
                {
                    throw new UnauthorizedAccessException(ErrorConstants.NotAuthorized);
                }

                return parsedUserId;
            }
        }

        /// <inheritdoc/>
        public string UserEmail
        {
            get
            {
                var email = _httpContextAccessor
                    .HttpContext
                    .User
                    ?.Claims
                    ?.SingleOrDefault(c =>
                        c.Type == ClaimTypes.Email)
                    ?.Value;

                if (email == null)
                {
                    throw new UnauthorizedAccessException(ErrorConstants.NotAuthorized);
                }

                return email;
            }
        }
    }
}
