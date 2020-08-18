using AutoMapper;
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
            CreateMap<CreateTaskViewModel, TaskDto>();

            CreateMap<CreateRouteViewModel, RouteDto>();
        }
    }
}
