using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripFlip.DataAccess;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto.Enums;
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
                throw new NotFoundException(errorMessage);
            }
        }

        /// <summary>
        /// Validates whether current user is trip admin.
        /// </summary>
        /// <param name="currentUserService">Instance of service class that provides access 
        /// to properties of the current user.</param>
        /// <param name="tripFlipDbContext">Instance of database context.</param>
        /// <param name="tripId">Trip id.</param>
        /// <param name="tripRoleToValidate">Trip role to validate.</param>
        /// <param name="errorMessage">Error message to show,
        /// if validation is not successful.</param>
        public static async Task ValidateCurrentUserTripRoleAsync(
            ICurrentUserService currentUserService,
            TripFlipDbContext tripFlipDbContext,
            int tripId,
            TripRoles tripRoleToValidate,
            string errorMessage)
        {
            var currentUserId = currentUserService.UserId;

            var tripSubscriberEntity = await tripFlipDbContext
                .TripSubscribers
                .AsNoTracking()
                .Include(tripSubscriber => tripSubscriber.TripRoles)
                .SingleOrDefaultAsync(tripSubscriber =>
                    tripSubscriber.UserId == currentUserId
                    && tripSubscriber.TripId == tripId);

            ValidateEntityNotNull(tripSubscriberEntity, 
                ErrorConstants.TripSubscriberNotFound);

            var tripSubscriberHasSpecifiedRole = tripSubscriberEntity
                .TripRoles
                .Any(tripRole =>
                tripRole.TripRoleId == (int)tripRoleToValidate);

            if (!tripSubscriberHasSpecifiedRole)
            {
                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// Validates whether current user is application super admin.
        /// </summary>
        /// <param name="currentUserService">Instance of service class that provides access 
        /// to properties of the current user.</param>
        /// <param name="tripFlipDbContext">Instance of database context.</param>
        public static async Task ValidateCurrentUserIsSuperAdminAsync(
            ICurrentUserService currentUserService,
            TripFlipDbContext tripFlipDbContext)
        {
            var currentUserId = currentUserService.UserId;

            var currentUser = await tripFlipDbContext
                .Users
                .AsNoTracking()
                .Include(user => user.ApplicationRoles)
                .SingleOrDefaultAsync(user => user.Id == currentUserId);
            
            ValidateEntityNotNull(currentUser, ErrorConstants.NotAuthorized);

            var currentUserIsSuperAdmin = currentUser
                .ApplicationRoles
                .Any(appRole =>
                appRole.ApplicationRoleId == (int)ApplicationRole.SuperAdmin);

            if (!currentUserIsSuperAdmin)
            {
                throw new ArgumentException(ErrorConstants.NotSuperAdmin);
            }
        }

        /// <summary>
        /// Validates whether current user has a specified route role.
        /// </summary>
        /// <param name="currentUserService">Instance of service class that provides access 
        /// to properties of the current user.</param>
        /// <param name="tripFlipDbContext">Instance of database context.</param>
        /// <param name="routeId">Route id.</param>
        /// <param name="routeRoleToValidate">Route role to validate.</param>
        /// <param name="errorMessage">Error message to show,
        /// if validation is not successful.</param>
        public static async Task ValidateCurrentUserRouteRoleAsync(
            ICurrentUserService currentUserService,
            TripFlipDbContext tripFlipDbContext,
            int routeId,
            RouteRoles routeRoleToValidate,
            string errorMessage)
        {
            var currentUserId = currentUserService.UserId;

            var routeSubscriberEntity = await tripFlipDbContext
                .RouteSubscribers
                .AsNoTracking()
                .Include(routeSubscriber => routeSubscriber.RouteRoles)
                .Include(routeSubscriber => routeSubscriber.TripSubscriber)
                .SingleOrDefaultAsync(routeSubscriber =>
                    routeSubscriber.TripSubscriber.UserId == currentUserId
                    && routeSubscriber.RouteId == routeId);

            ValidateEntityNotNull(routeSubscriberEntity,
                ErrorConstants.NotRouteSubscriber);

            var routeSubscriberHasSpecifiedRole = routeSubscriberEntity
                .RouteRoles
                .Any(routeRole =>
                routeRole.RouteRoleId == (int)routeRoleToValidate);

            if (!routeSubscriberHasSpecifiedRole)
            {
                throw new ArgumentException(errorMessage);
            }
        }
    }
}
