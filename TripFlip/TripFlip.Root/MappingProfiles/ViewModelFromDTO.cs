using AutoMapper;
using TripFlip.Services.DTO;
using TripFlip.ViewModels;

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
