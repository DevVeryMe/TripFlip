using AutoMapper;
using GoogleAuthentication.Domain.Entities;
using GoogleAuthentication.Services.Dtos;

namespace GoogleAuthentication.Root.MappingProfiles
{
    public class EntityToDto : Profile
    {
        public EntityToDto()
        {
            CreateMap<UserEntity, AuthenticatedUserDto>();

            CreateMap<UserEntity, UserDto>();
        }
    }
}
