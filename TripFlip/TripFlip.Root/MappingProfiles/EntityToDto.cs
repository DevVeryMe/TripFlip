using System.Linq;
using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Dto.RoutePointDtos;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Dto.TaskListDtos;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Dto.TripRoleDtos;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Root.MappingProfiles
{
    public class EntityToDto : Profile
    {
        public EntityToDto()
        {
            CreateMap<TripEntity, TripDto>();

            CreateMap<PagedList<TripEntity>, PagedList<TripDto>>();

            CreateMap<ItemEntity, ItemDto>();

            CreateMap<TaskEntity, TaskDto>();

            CreateMap<ItemEntity, ItemWithoutListIdDto>();

            CreateMap<TaskEntity, TaskWithoutListIdDto>();

            CreateMap<TaskListEntity, TaskListDto>();

            CreateMap<RouteEntity, RouteDto>();

            CreateMap<PagedList<RouteEntity>, PagedList<RouteDto>>();

            CreateMap<ItemListEntity, ItemListDto>();
            
            CreateMap<PagedList<ItemEntity>, PagedList<ItemDto>>();

            CreateMap<PagedList<ItemListEntity>, PagedList<ItemListDto>>();

            CreateMap<PagedList<TaskEntity>, PagedList<TaskDto>>();

            CreateMap<PagedList<TaskListEntity>, PagedList<TaskListDto>>();

            CreateMap<UserEntity, UserDto>();

            CreateMap<UserEntity, AuthenticatedUserDto>();

            CreateMap<PagedList<UserEntity>, PagedList<UserDto>>();

            CreateMap<ItemListEntity, ItemListWithItemsDto>();

            CreateMap<TaskListEntity, TaskListWithTasksDto>();

            CreateMap<RoutePointEntity, RoutePointDto>();

            CreateMap<RouteEntity, RouteWithPointsItemAndTaskListsDto>();

            CreateMap<TripSubscriberEntity, TripWithRoutesDto>()
                .ForMember(destination => destination.Title,
                    configurationExpression =>
                        configurationExpression.MapFrom(tripSubscriberEntity => tripSubscriberEntity.Trip.Title))
                .ForMember(destination => destination.Description,
                    configurationExpression =>
                        configurationExpression.MapFrom(tripSubscriberEntity => tripSubscriberEntity.Trip.Description))
                .ForMember(destination => destination.Id,
                    configurationExpression =>
                        configurationExpression.MapFrom(tripSubscriberEntity => tripSubscriberEntity.Trip.Id))
                .ForMember(destination => destination.StartsAt,
                    configurationExpression =>
                        configurationExpression.MapFrom(tripSubscriberEntity => tripSubscriberEntity.Trip.StartsAt))
                .ForMember(destination => destination.EndsAt,
                    configurationExpression =>
                        configurationExpression.MapFrom(tripSubscriberEntity => tripSubscriberEntity.Trip.EndsAt))
                .ForMember(destination => destination.Routes,
                    configurationExpression =>
                        configurationExpression.MapFrom(tripSubscriberEntity => tripSubscriberEntity.Trip.Routes))
                .ForMember(destination => destination.TripRoles,
                    configurationExpression =>
                        configurationExpression.MapFrom(tripSubscriberEntity => tripSubscriberEntity.TripRoles
                            .Select(role => new TripRoleDto() { Id = role.TripRole.Id, Name = role.TripRole.Name })));
        }
    }
}
