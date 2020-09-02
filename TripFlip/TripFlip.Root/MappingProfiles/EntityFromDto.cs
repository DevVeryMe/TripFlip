using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Dto.TaskListDtos;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Dto.UserDtos;

namespace TripFlip.Root.MappingProfiles
{
    public class EntityFromDto : Profile
    {
        public EntityFromDto()
        {
            CreateMap<TripDto, TripEntity>();

            CreateMap<CreateTripDto, TripEntity>();

            CreateMap<CreateItemDto, ItemEntity>();

            CreateMap<TaskDto, TaskEntity>();

            CreateMap<CreateTaskDto, TaskEntity>();

            CreateMap<CreateTaskListDto, TaskListEntity>();

            CreateMap<TaskListDto, TaskListEntity>();

            CreateMap<CreateRouteDto, RouteEntity>();

            CreateMap<ItemDto, ItemEntity>();

            CreateMap<CreateItemListDto, ItemListEntity>();

            CreateMap<UserDto, UserEntity>();
        }
    }
}
