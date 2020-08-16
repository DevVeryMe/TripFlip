using AutoMapper;
using TripFlip.Services.DTO;
using TripFlip.ViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<CreateTaskViewModel, TaskDto>();
        }
    }
}
