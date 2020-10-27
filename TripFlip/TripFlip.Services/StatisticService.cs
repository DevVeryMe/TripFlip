using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Enums;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class StatisticService : IStatisticService
    {
        private readonly TripFlipDbContext _tripFlipDbContext;

        /// <summary>
        /// Initializes database context.
        /// </summary>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        public StatisticService(TripFlipDbContext tripFlipDbContext)
        {
            _tripFlipDbContext = tripFlipDbContext;
        }

        public async Task<UserStatisticModel> GetUserStatisticById(Guid userId)
        {
            var userEntity = await GetUserEntityIncludingSubEntitiesForStatistic(userId);

            return GetUserStatisticModel(userEntity);
        }

        /// <summary>
        /// Finds database entry about user specified by id, including:
        /// <list type="bullet">
        /// <item>trip subscriptions</item>
        /// <item>route subscriptions</item>
        /// <item>trips</item>
        /// <item>routes and their subscribers</item>
        /// <item>trip roles</item>
        /// <item>route roles</item>
        /// <item>assigned tasks</item>
        /// <item>assigned items with item lists</item>
        /// </list>
        /// </summary>
        /// <param name="userId">Id of user to find.</param>
        /// <returns>Found user entity.</returns>
        private async Task<UserEntity> GetUserEntityIncludingSubEntitiesForStatistic(Guid userId)
        {
            var userEntity = await _tripFlipDbContext
                .Users
                .Include(user => user.TripSubscriptions)
                    .ThenInclude(tripSubscriber => tripSubscriber.TripRoles)
                .Include(user => user.TripSubscriptions)
                    .ThenInclude(tripSubscriber => tripSubscriber.Trip)
                        .ThenInclude(trip => trip.Routes)
                            .ThenInclude(route => route.RouteSubscribers)
                .Include(user => user.TripSubscriptions)
                    .ThenInclude(tripSubscription => tripSubscription.RouteSubscriptions)
                        .ThenInclude(routeSubscription => routeSubscription.RouteRoles)
                .Include(user => user.TripSubscriptions)
                    .ThenInclude(tripSubscription => tripSubscription.RouteSubscriptions)
                        .ThenInclude(routeSubscription => routeSubscription.AssignedTasks)
                            .ThenInclude(assignedTask => assignedTask.Task)
                .Include(user => user.TripSubscriptions)
                    .ThenInclude(tripSubscription => tripSubscription.RouteSubscriptions)
                        .ThenInclude(routeSubscription => routeSubscription.AssignedItems)
                            .ThenInclude(assignedItem => assignedItem.Item)
                                .ThenInclude(item => item.ItemList)
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == userId);

            EntityValidationHelper.ValidateEntityNotNull(userEntity, ErrorConstants.UserNotFound);

            return userEntity;
        }

        /// <summary>
        /// Creates UserStatisticModel instance and sets all the statistic data.
        /// </summary>
        /// <param name="userEntity">User to get statistic for.</param>
        /// <returns>UserStatisticModel instance, which represents user statistic data.</returns>
        private static UserStatisticModel GetUserStatisticModel(UserEntity userEntity)
        {
            var oneMonthAgo = DateTimeOffset.Now.AddMonths(-1);

            var lastMonthRouteSubscriptions = userEntity
                .TripSubscriptions
                .SelectMany(tripSubscription => 
                    tripSubscription.RouteSubscriptions
                        .Where(routeSubscription => 
                            routeSubscription.DateSubscribed >= oneMonthAgo)).ToList();

            var totalRouteSubscriptions = userEntity
                .TripSubscriptions
                .SelectMany(tripSubscription => tripSubscription.RouteSubscriptions).ToList();

            var tripSubscriptions = userEntity
                .TripSubscriptions
                .ToList();

            var userStatisticModel = new UserStatisticModel()
            {
                Email = userEntity.Email,

                FirstName = userEntity.FirstName,

                LastMonthTripsWhereHasAdminRoleCount =
                    CountTripsWhereHasAdminRole(tripSubscriptions, oneMonthAgo),

                LastMonthRoutesWhereHasAdminRoleCount =
                    CountRoutesWhereHasAdminRole(lastMonthRouteSubscriptions),

                LastMonthRoutesWhereNotAdminCount =
                    CountRoutesWhereNotAdmin(lastMonthRouteSubscriptions),

                LastMonthCompletedTasksCount =
                    CountCompletedTasksForPeriod(lastMonthRouteSubscriptions, oneMonthAgo),

                LastMonthCompletedItemsCount =
                    CountCompletedItemsForPeriod(lastMonthRouteSubscriptions, oneMonthAgo),

                LastMonthUsersInHisRoutesCount =
                    CountUsersInHisRoutes(tripSubscriptions, oneMonthAgo),

                TotalTripsWhereHasAdminRoleCount =
                    CountTripsWhereHasAdminRole(tripSubscriptions, DateTimeOffset.MinValue),

                TotalRoutesWhereHasAdminRoleCount = 
                    CountRoutesWhereHasAdminRole(totalRouteSubscriptions),

                TotalRoutesWhereNotAdminCount = 
                    CountRoutesWhereNotAdmin(totalRouteSubscriptions),

                TotalCompletedTasksCount = 
                    CountCompletedTasksForPeriod(totalRouteSubscriptions, DateTimeOffset.MinValue),

                TotalCompletedItemsCount = 
                    CountCompletedItemsForPeriod(totalRouteSubscriptions, DateTimeOffset.MinValue),

                TotalUsersInHisRoutesCount = 
                    CountUsersInHisRoutes(tripSubscriptions, DateTimeOffset.MinValue)
            };

            return userStatisticModel;
        }

        private static int CountRoutesWhereHasAdminRole(IEnumerable<RouteSubscriberEntity> routeSubscriptions)
        {
            return routeSubscriptions.Count(routeSubscription =>
                routeSubscription.RouteRoles.Any(routeRole => routeRole.RouteRoleId == (int) RouteRoles.Admin));
        }

        private static int CountRoutesWhereNotAdmin(IEnumerable<RouteSubscriberEntity> routeSubscriptions)
        {
            return routeSubscriptions.Count(routeSubscription =>
                routeSubscription.RouteRoles.All(routeRole => routeRole.RouteRoleId != (int)RouteRoles.Admin));
        }

        private static int CountCompletedTasksForPeriod(IEnumerable<RouteSubscriberEntity> routeSubscriptions,
            DateTimeOffset startDate)
        {
            return routeSubscriptions.Sum(routeSubscription =>
                routeSubscription.AssignedTasks
                    .Where(assignedTask => assignedTask.Task.DateCreated >= startDate)
                    .Count(assignedTask => assignedTask.Task.IsCompleted));
        }

        private static int CountCompletedItemsForPeriod(IEnumerable<RouteSubscriberEntity> routeSubscriptions,
            DateTimeOffset startDate)
        {
            return routeSubscriptions.Sum(routeSubscription =>
                routeSubscription.AssignedItems
                    .Where(assignedItem => assignedItem.Item.ItemList.DateCreated >= startDate)
                    .Count(assignedItem => assignedItem.Item.IsCompleted));
        }

        private static int CountTripsWhereHasAdminRole(
            IEnumerable<TripSubscriberEntity> tripSubscriptions,
            DateTimeOffset startDate)
        {
            return tripSubscriptions
                .Where(tripSubscriber => tripSubscriber.DateSubscribed >= startDate)
                .Count(tripSubscriber => tripSubscriber.TripRoles.Any(
                    tripSubscriberRole => tripSubscriberRole.TripRoleId == (int)TripRoles.Admin));
        }

        private static int CountUsersInHisRoutes(
            IEnumerable<TripSubscriberEntity> tripSubscriptions,
            DateTimeOffset startDate)
        {
            var tripSubscriptionsWhereUserIsAdmin = tripSubscriptions
                .Where(tripSubscriber => tripSubscriber.DateSubscribed >= startDate)
                .Where(tripSubscriber => tripSubscriber.TripRoles.Any(
                    tripSubscriberRole => tripSubscriberRole.TripRoleId == (int)TripRoles.Admin));

            int usersCount = 0;

            foreach (var tripSubscription in tripSubscriptionsWhereUserIsAdmin)
            {
                int tripSubscriberIdOfCurrentUser = tripSubscription.Id;

                var routesOfTripWhereUserIsAdmin = tripSubscription.Trip.Routes;

                usersCount += routesOfTripWhereUserIsAdmin
                    .Sum(route => route.RouteSubscribers.Count(
                        routeSubscriber => routeSubscriber.TripSubscriberId != tripSubscriberIdOfCurrentUser));
            }

            return usersCount;
        }
    }
}
