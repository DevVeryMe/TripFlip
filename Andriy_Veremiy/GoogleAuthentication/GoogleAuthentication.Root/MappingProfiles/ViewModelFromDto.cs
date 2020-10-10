using AutoMapper;
using GoogleAuthentication.Services.Dtos;
using GoogleAuthentication.ViewModels;

namespace GoogleAuthentication.Root.MappingProfiles
{
    public class ViewModelFromDto : Profile
    {
        public ViewModelFromDto()
        {
            CreateMap<AuthenticatedUserDto, AuthenticatedUserViewModel>();
        }
    }
}
