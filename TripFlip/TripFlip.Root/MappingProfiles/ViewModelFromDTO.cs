using AutoMapper;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.Services.DTO.RouteDtos;
using TripFlip.ViewModels.TripViewModels;
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

            CreateMap<CreateRouteDto, ResultRouteViewModel>();
            CreateMap<UpdateRouteDto, ResultRouteViewModel>();
        }
    }
}
