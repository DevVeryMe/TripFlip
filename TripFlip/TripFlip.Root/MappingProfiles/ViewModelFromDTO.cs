using AutoMapper;
using TripFlip.Services.DTO;
using TripFlip.ViewModels;
using TripFlip.ViewModels.RouteViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelFromDto : Profile
    {
        public ViewModelFromDto()
        {
            CreateMap<TripDto, TripViewModel>();

            CreateMap<RouteDto, CreateRouteViewModel>();
            CreateMap<RouteDto, ResultRouteViewModel>();
        }
    }
}
