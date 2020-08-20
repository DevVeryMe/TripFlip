using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO.ItemListDtos;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.Services.DTO.RouteDtos;

namespace TripFlip.Root.MappingProfiles
{
    public class EntityToDto : Profile
    {
        public EntityToDto()
        {
            CreateMap<TripEntity, TripDto>();

            CreateMap<ItemEntity, ItemDto>();

            CreateMap<TaskEntity, TaskDto>();

            CreateMap<RouteEntity, ResultRouteDto>();

            CreateMap<ItemListEntity, ResultItemListDto>();
        }
    }
}
