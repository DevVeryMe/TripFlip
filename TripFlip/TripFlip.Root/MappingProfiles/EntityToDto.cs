using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Dto.TaskListDtos;
using TripFlip.Services.Dto.TripDtos;
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

            CreateMap<RouteEntity, ResultRouteDto>();

            CreateMap<PagedList<RouteEntity>, PagedList<ResultRouteDto>>();

            CreateMap<ItemListEntity, ResultItemListDto>();
            
            CreateMap<PagedList<ItemEntity>, PagedList<ItemDto>>();

            CreateMap<PagedList<ItemListEntity>, PagedList<ResultItemListDto>>();

            CreateMap<PagedList<TaskEntity>, PagedList<TaskDto>>();

            CreateMap<PagedList<TaskListEntity>, PagedList<TaskListDto>>();
        }
    }
}
