using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripFlip.DataAccess;
using TripFlip.Services.Enums;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services.Helpers
{
    public static class EntityValidationHelper
    {
        /// <summary>
        /// Validates whether entity is not null. If null,
        /// throws an ArgumentException.
        /// </summary>
        /// <typeparam name="TEntity">Any entity to check.</typeparam>
        /// <param name="entity">Instance of TEntity.</param>
        /// <param name="errorMessage">Error message to display.</param>
        public static void ValidateEntityNotNull<TEntity>(TEntity entity, string errorMessage)
        {
            if (entity is null)
            {
                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// Validates whether current user is trip admin.
        /// </summary>
        /// <param name="currentUserService">Instance of service class that provides access 
        /// to properties of the current user.</param>
        /// <param name="tripFlipDbContext">Instance of database context.</param>
        /// <param name="tripId">Trip id.</param>
        /// <returns></returns>
        public static async Task ValidateCurrentUserIsTripAdminAsync(
            ICurrentUserService currentUserService,
            TripFlipDbContext tripFlipDbContext,
            int tripId)
        {
            var currentUserIdString = currentUserService.UserId;
            var currentUserId = Guid.Parse(currentUserIdString);

            var tripSubscriberEntity = await tripFlipDbContext
                .TripSubscribers
                .AsNoTracking()
                .Include(tripSubscriber => tripSubscriber.TripRoles)
                .SingleOrDefaultAsync(tripSubscriber =>
                    tripSubscriber.UserId == currentUserId
                    && tripSubscriber.TripId == tripId);

            ValidateEntityNotNull(tripSubscriberEntity, 
                ErrorConstants.TripSubscriberNotFound);

            var tripSubscriberIsAdmin = tripSubscriberEntity
                .TripRoles
                .Any(tripRole =>
                tripRole.TripRoleId == (int)TripRoles.Admin);

            if (!tripSubscriberIsAdmin)
            {
                throw new ArgumentException(ErrorConstants.NotTripAdmin);
            }
        }
    }
}
