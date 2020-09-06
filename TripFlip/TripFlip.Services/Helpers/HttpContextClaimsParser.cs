﻿using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace TripFlip.Services.Helpers
{
    static class HttpContextClaimsParser
    {
        /// <summary>
        /// Gets users id from http request user claims.
        /// </summary>
        /// <returns>The users id.</returns>
        public static Guid GetUserIdFromClaims(IHttpContextAccessor httpContextAccessor)
        {
            var currentUserIdToParse = httpContextAccessor
                .HttpContext
                .User
                ?.Claims
                ?.SingleOrDefault(c =>
                    c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                ?.Value;

            if (currentUserIdToParse is null)
            {
                throw new UnauthorizedAccessException();
            }

            var currentUserId = Guid.Parse(currentUserIdToParse);

            return currentUserId;
        }
    }
}
