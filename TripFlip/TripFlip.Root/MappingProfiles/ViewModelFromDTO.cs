using AutoMapper;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.DTO.ItemListDtos;
using TripFlip.Services.DTO.RouteDtos;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.DTO.TaskListDtos;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.ViewModels.ItemListViewModels;
using TripFlip.ViewModels.ItemViewModels;
using TripFlip.ViewModels.RouteViewModels;
using TripFlip.ViewModels.TaskListViewModels;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.ViewModels.TripViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelFromDto : Profile
    {
        public ViewModelFromDto()
        {
            CreateMap<TripDto, TripViewModel>();
            CreateMap<PagedList<TripDto>, PagedList<TripViewModel>>();

            CreateMap<TaskDto, GetTaskViewModel>();
            CreateMap<TaskDto, UpdateTaskViewModel>();

            CreateMap<TaskListDto, GetTaskListViewModel>();
            CreateMap<TaskListDto, UpdateTaskListViewModel>();

            CreateMap<ItemDto, ItemViewModel>();

            CreateMap<ResultRouteDto, ResultRouteViewModel>();
            CreateMap<PagedList<ResultRouteDto>, PagedList<ResultRouteViewModel>>();

            CreateMap<ResultItemListDto, ResultItemListViewModel>();

            CreateMap<PagedList<ItemDto>, PagedList<ItemViewModel>>();

            CreateMap<PagedList<ResultItemListDto>, PagedList<ResultItemListViewModel>>();

            CreateMap<PagedList<TaskDto>, PagedList<GetTaskViewModel>>();

            CreateMap<PagedList<TaskListDto>, PagedList<GetTaskListViewModel>>();
        }
    }
}
