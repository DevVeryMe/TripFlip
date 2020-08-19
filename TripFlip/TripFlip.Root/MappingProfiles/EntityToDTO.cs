using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.DTO.TripDtos;

namespace TripFlip.Root.MappingProfiles
{
    public class EntityToDto : Profile
    {
        public EntityToDto()
        {
            CreateMap<TripEntity, TripDto>();

            CreateMap<TaskEntity, TaskDto>();

            CreateMap<RouteEntity, RouteDto>();
        }
    }
}
