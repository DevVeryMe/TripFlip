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

            CreateMap<ItemListEntity, ItemListWithIncludesDto>();

            CreateMap<TaskListEntity, TaskListWithIncludesDto>();

            CreateMap<RoutePointEntity, RoutePointDto>();

            CreateMap<RouteEntity, RouteWithIncludesDto>();

            CreateMap<TripSubscriberRoleEntity, TripRoleDto>()
                .ForMember(x => x.Id, 
                    a => 
                        a.MapFrom(z => z.TripRole.Id))
                .ForMember(x => x.Name,
                    a =>
                        a.MapFrom(z => z.TripRole.Name));

            CreateMap<TripEntity, TripWithIncludesDto>();

            CreateMap<TripSubscriberEntity, TripWithRolesDto>();
        }
    }
}
