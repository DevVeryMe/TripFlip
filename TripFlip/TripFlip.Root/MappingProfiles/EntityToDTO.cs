﻿using AutoMapper;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;

namespace TripFlip.Root.MappingProfiles
{
    public class EntityToDto : Profile
    {
        public EntityToDto()
        {
            CreateMap<TripEntity, TripDto>();

            CreateMap<RouteEntity, RouteDto>();
        }
    }
}
