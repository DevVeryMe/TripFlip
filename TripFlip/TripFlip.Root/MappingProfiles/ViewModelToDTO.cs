using AutoMapper;
using TripFlip.Services.DTO;
using TripFlip.ViewModels;
using TripFlip.ViewModels.RouteViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<CreateRouteViewModel, RouteDto>();
        }
    }
}
