using AutoMapper;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.ViewModels.ItemViewModels;
using TripFlip.ViewModels.TaskViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<TripViewModel, TripDto>();

            CreateMap<CreateTaskViewModel, TaskDto>();

            CreateMap<CreateTripViewModel, CreateTripDto>();

            CreateMap<CreateItemViewModel, CreateItemDto>();
        }
    }
}
