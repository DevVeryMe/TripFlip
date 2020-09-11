using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
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
        public string UserId
        {
            get
            {
                return _httpContextAccessor
                    .HttpContext
                    .User
                    ?.Claims
                    ?.SingleOrDefault(c =>
                        c.Type == ClaimTypes.NameIdentifier)
                    ?.Value;
            }
        }

        /// <inheritdoc/>
        public string UserEmail
        {
            get
            {
                return _httpContextAccessor
                    .HttpContext
                    .User
                    ?.Claims
                    ?.SingleOrDefault(c =>
                        c.Type == ClaimTypes.Email)
                    ?.Value;
            }
        }
    }
}
