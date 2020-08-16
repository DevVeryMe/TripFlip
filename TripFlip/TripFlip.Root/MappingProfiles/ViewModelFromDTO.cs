using AutoMapper;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.ViewModels;
using TripFlip.ViewModels.TripViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelFromDto : Profile
    {
        public ViewModelFromDto()
        {
            CreateMap<TripDto, TripViewModel>();
        }
    }
}
