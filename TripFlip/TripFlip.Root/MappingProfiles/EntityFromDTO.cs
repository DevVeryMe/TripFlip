using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.Services.DTO.RouteDtos;

namespace TripFlip.Root.MappingProfiles
{
    public class EntityFromDto : Profile
    {
        public EntityFromDto()
        {
            CreateMap<TripDto, TripEntity>();

            CreateMap<CreateTripDto, TripEntity>();

            CreateMap<TaskDto, TaskEntity>();

            CreateMap<CreateRouteDto, RouteEntity>();
        }
    }
}
