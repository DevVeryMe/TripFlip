using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;

namespace TripFlip.Root.MappingProfiles
{
    public class EntityFromDto : Profile
    {
        public EntityFromDto()
        {
            CreateMap<TripDto, TripEntity>();
        }
    }
}
