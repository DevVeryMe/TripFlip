using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.DTO.TaskListDtos;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.Services.DTO.RouteDtos;
using TripFlip.Services.DTO.ItemListDtos;

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

            CreateMap<CreateTaskListDto, TaskListEntity>();

            CreateMap<CreateRouteDto, RouteEntity>();

            CreateMap<ItemDto, ItemEntity>();

            CreateMap<CreateItemListDto, ItemListEntity>();
        }
    }
}
