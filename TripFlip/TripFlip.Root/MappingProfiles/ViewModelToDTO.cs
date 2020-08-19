using AutoMapper;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.ViewModels;
using TripFlip.ViewModels.RouteViewModels;
using TripFlip.Services.DTO;

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

            CreateMap<CreateRouteViewModel, RouteDto>();
            CreateMap<UpdateRouteViewModel, RouteDto>();
            CreateMap<ResultRouteViewModel, RouteDto>();
        }
    }
}
