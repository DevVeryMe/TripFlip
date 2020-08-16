using AutoMapper;
using TripFlip.Services.DTO;
using TripFlip.ViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelToDTO : Profile
    {
        public ViewModelToDTO()
        {
            CreateMap<TaskViewModel, TaskDto>();
        }
    }
}
