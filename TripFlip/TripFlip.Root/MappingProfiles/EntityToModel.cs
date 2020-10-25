using AutoMapper;
using System.Linq;
using TripFlip.Domain.Entities;
using TripFlip.Services.Enums;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Root.MappingProfiles
{
    public class EntityToModel : Profile
    {
        public EntityToModel()
        {
            CreateMap<UserEntity, UserStatisticsModel>()
                .ForMember(destination => destination.Email,
                    configurationExpression =>
                        configurationExpression.MapFrom(
                            userEntity => userEntity.Email))
                .ForMember(destination => destination.CompletedItemsCount,
                    configurationExpression =>
                        configurationExpression.MapFrom(
                            userEntity => userEntity.TripSubscriptions.Sum(tripSubscription =>
                                tripSubscription.RouteSubscriptions.Sum(routeSubscription =>
                                    routeSubscription.AssignedItems.Count(assignedItem =>
                                        assignedItem.Item.IsCompleted)))))
                .ForMember(destination => destination.CompletedTasksCount,
                    configurationExpression =>
                        configurationExpression.MapFrom(
                            userEntity => userEntity.TripSubscriptions.Sum(tripSubscription =>
                                tripSubscription.RouteSubscriptions.Sum(routeSubscription =>
                                    routeSubscription.AssignedTasks.Count(assignedTask =>
                                        assignedTask.Task.IsCompleted)))))
                .ForMember(destination => destination.RoutesCountWhereHasAdminRole,
                    configurationExpression =>
                        configurationExpression.MapFrom(
                            userEntity => userEntity.TripSubscriptions.Sum(tripSubscription =>
                                tripSubscription.RouteSubscriptions.Sum(routeSubscription =>
                                    routeSubscription.RouteRoles.Count(routeRole =>
                                        routeRole.RouteRoleId == (int) RouteRoles.Admin)))))
                .ForMember(destination => destination.RoutesCountWhereHasAdminRole,
                    configurationExpression =>
                        configurationExpression.MapFrom(
                            userEntity => userEntity.TripSubscriptions.Sum(tripSubscription =>
                                tripSubscription.RouteSubscriptions.Sum(routeSubscription =>
                                    routeSubscription.RouteRoles.Count(routeRole =>
                                        routeRole.RouteRoleId != (int) RouteRoles.Admin)))));
        }
    }
}
