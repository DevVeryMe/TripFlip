using AutoMapper;

namespace TripFlip.Root.MappingProfiles
{
    public class EnumToEnum : Profile
    {
        public EnumToEnum()
        {
            CreateMap<Services.DTO.Enums.TaskPriorityLevel, Domain.Entities.Enums.TaskPriorityLevel>();
        }
    }
}
