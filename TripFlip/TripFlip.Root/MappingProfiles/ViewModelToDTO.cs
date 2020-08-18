using AutoMapper;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.Services.DTO;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.ViewModels;
using TripFlip.ViewModels.RouteViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<TripViewModel, TripDto>();

            CreateMap<CreateTaskViewModel, TaskDto>();

            CreateMap<CreateTripViewModel, CreateTripDto>();

            CreateMap<CreateRouteViewModel, RouteDto>();
            CreateMap<UpdateRouteViewModel, RouteDto>();
            CreateMap<ResultRouteViewModel, RouteDto>();
        }
    }
}
