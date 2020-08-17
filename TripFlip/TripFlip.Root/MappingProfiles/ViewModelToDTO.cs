using AutoMapper;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.ViewModels.TaskViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<TripViewModel, TripDto>();

            CreateMap<CreateTripViewModel, CreateTripDto>();

            CreateMap<CreateTaskViewModel, TaskDto>();

            CreateMap<UpdateTaskViewModel, TaskDto>();
        }
    }
}
