using AutoMapper;
using TripFlip.Services.DTO;
using TripFlip.ViewModels;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.ViewModels.RouteViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelFromDto : Profile
    {
        public ViewModelFromDto()
        {
            CreateMap<TripDto, TripViewModel>();

            CreateMap<TaskDto, GetTaskViewModel>();

            CreateMap<RouteDto, CreateRouteViewModel>();

            CreateMap<RouteDto, ResultRouteViewModel>();
        }
    }
}
