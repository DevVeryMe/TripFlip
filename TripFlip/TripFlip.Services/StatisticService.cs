using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<UserStatisticModel> GetUserMonthStatisticById(Guid userId)
        {
            var oneMonthAgo = DateTimeOffset.Now.AddMonths(-1);

            return await GetUserStatisticModel(userId, oneMonthAgo);
        }

        public async Task<UserStatisticModel> GetUserTotalStatisticById(Guid userId)
        {
            return await GetUserStatisticModel(userId, DateTimeOffset.MinValue);
        }

        /// <summary>
        /// Finds database user entry by id including trip subscriptions, route subscriptions,
        /// route roles, assigned tasks and items with item lists.
        /// </summary>
        /// <param name="userId">Id of user to find.</param>
        /// <returns>Found user entity.</returns>
        private async Task<UserEntity> GetUserEntityIncludingSubEntitiesForStatistic(Guid userId)
        {
            var userEntity = await _tripFlipDbContext
                .Users
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
        /// Creates UserStatisticsModel instance and sets statistics data.
        /// </summary>
        /// <param name="userId">Id of user to find.</param>
        /// <param name="statisticsFrom">Start date to get statistics from.</param>
        /// <returns>UserStatisticsModel instance, which represents user statistics data.</returns>
        private async Task<UserStatisticModel> GetUserStatisticModel(Guid userId, DateTimeOffset statisticsFrom)
        {
            var userEntity = await GetUserEntityIncludingSubEntitiesForStatistic(userId);

            var routeSubscriptions = userEntity.TripSubscriptions.SelectMany(tripSubscription =>
                tripSubscription.RouteSubscriptions.Where(routeSubscription =>
                    routeSubscription.DateSubscribed >= statisticsFrom)).ToList();

            var userStatisticModel = new UserStatisticModel()
            {
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                RoutesWhereHasAdminRoleCount = routeSubscriptions.Count(
                    routeSubscription =>
                        routeSubscription.RouteRoles.Any(routeRole =>
                            routeRole.RouteRoleId == (int) RouteRoles.Admin)),
                RoutesWhereNotAdminCount = routeSubscriptions.Count(
                    routeSubscription =>
                        routeSubscription.RouteRoles.All(routeRole =>
                            routeRole.RouteRoleId != (int) RouteRoles.Admin)),
                CompletedTasksCount = routeSubscriptions.Sum(routeSubscription =>
                    routeSubscription.AssignedTasks
                        .Where(assignedTask => assignedTask.Task.DateCreated >= statisticsFrom).Count(
                            assignedTask =>
                                assignedTask.Task.IsCompleted)),
                CompletedItemsCount = routeSubscriptions.Sum(routeSubscription =>
                    routeSubscription.AssignedItems
                        .Where(assignedItem => assignedItem.Item.ItemList.DateCreated >= statisticsFrom).Count(
                            assignedItem =>
                                assignedItem.Item.IsCompleted))
            };

            return userStatisticModel;
        }
    }
}
