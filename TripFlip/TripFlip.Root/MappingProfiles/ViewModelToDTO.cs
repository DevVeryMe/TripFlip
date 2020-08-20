using AutoMapper;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.ViewModels.ItemViewModels;
using TripFlip.Services.DTO.RouteDtos;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.Services.DTO.TaskListDtos;
using TripFlip.ViewModels.TaskListViewModels;
using TripFlip.ViewModels.RouteViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<TripViewModel, TripDto>();

            CreateMap<CreateTripViewModel, CreateTripDto>();

            CreateMap<CreateTaskViewModel, TaskDto>();

            CreateMap<UpdateTaskViewModel, UpdateTaskDto>();

            CreateMap<CreateTaskListViewModel, CreateTaskListDto>();

            CreateMap<CreateItemViewModel, CreateItemDto>();

            CreateMap<UpdateItemViewModel, UpdateItemDto>();

            CreateMap<CreateRouteViewModel, CreateRouteDto>();

            CreateMap<UpdateRouteViewModel, UpdateRouteDto>();
        }
    }
}
