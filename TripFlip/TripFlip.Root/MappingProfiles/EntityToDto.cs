using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Dto.TaskListDtos;
using TripFlip.Services.Dto.TripDtos;
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
        }
    }
}
