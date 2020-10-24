using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IMapper _mapper;

        private readonly TripFlipDbContext _tripFlipDbContext;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        public StatisticsService(TripFlipDbContext tripFlipDbContext,
            IMapper mapper)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
        }

        public async Task<UserStatisticsModel> GetUserMonthStatisticsById(Guid userId)
        {
            var oneMonthAgo = DateTimeOffset.Now.AddMonths(-1);

            return await GetUserStatisticsModel(userId, oneMonthAgo);
        }

        public async Task<UserStatisticsModel> GetUserTotalStatisticsById(Guid userId)
        {
            return await GetUserStatisticsModel(userId, DateTimeOffset.MinValue);
        }

        private async Task<UserStatisticsModel> GetUserStatisticsModel(Guid userId, DateTimeOffset statisticsFrom)
        {
            var userEntity = await _tripFlipDbContext
                .Users
                .Include(user => user.TripSubscriptions)
                    .ThenInclude(tripSubscription => tripSubscription.RouteSubscriptions
                        .Where(routeSubscription => 
                            routeSubscription.DateSubscribed >= statisticsFrom))
                        .ThenInclude(routeSubscription => routeSubscription.RouteRoles)
                .Include(user => user.TripSubscriptions)
                    .ThenInclude(tripSubscription => tripSubscription.RouteSubscriptions)
                        .ThenInclude(routeSubscription =>
                            routeSubscription.AssignedTasks
                                .Where(assignedTask =>
                                    assignedTask.Task.IsCompleted && assignedTask.Task.DateCreated >= statisticsFrom))
                .Include(user => user.TripSubscriptions)
                    .ThenInclude(tripSubscription => tripSubscription.RouteSubscriptions)
                        .ThenInclude(routeSubscription =>
                            routeSubscription.AssignedItems
                                .Where(assignedItem => assignedItem.Item.IsCompleted))
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == userId);

            EntityValidationHelper.ValidateEntityNotNull(userEntity, ErrorConstants.UserNotFound);

            var userStatisticsModel = _mapper.Map<UserStatisticsModel>(userEntity);

            return userStatisticsModel;
        }
    }
}
