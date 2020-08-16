using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TripDtos;

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
