using AutoMapper;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.Services.DTO.RouteDtos;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.ViewModels.RouteViewModels;
using TripFlip.Services.DTO;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelFromDto : Profile
    {
        public ViewModelFromDto()
        {
            CreateMap<TripDto, TripViewModel>();

            CreateMap<TaskDto, GetTaskViewModel>();
            CreateMap<TaskDto, UpdateTaskViewModel>();

            CreateMap<ResultRouteDto, ResultRouteViewModel>();
        }
    }
}
