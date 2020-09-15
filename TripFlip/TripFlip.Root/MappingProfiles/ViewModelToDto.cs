using AutoMapper;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Dto.TaskListDtos;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.ViewModels;
using TripFlip.ViewModels.ItemListViewModels;
using TripFlip.ViewModels.ItemViewModels;
using TripFlip.ViewModels.RouteViewModels;
using TripFlip.ViewModels.TaskListViewModels;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.ViewModels.UserViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<TripViewModel, TripDto>();

            CreateMap<CreateTripViewModel, CreateTripDto>();

            CreateMap<UpdateTripViewModel, UpdateTripDto>();

            CreateMap<CreateTaskViewModel, CreateTaskDto>();

            CreateMap<UpdateTaskViewModel, UpdateTaskDto>();

            CreateMap<UpdateTaskPriorityViewModel, UpdateTaskPriorityDto>();

            CreateMap<UpdateTaskCompletenessViewModel, UpdateTaskCompletenessDto>();

            CreateMap<CreateTaskListViewModel, CreateTaskListDto>();

            CreateMap<UpdateTaskListViewModel, UpdateTaskListDto>();

            CreateMap<CreateItemListViewModel, CreateItemListDto>();

            CreateMap<UpdateItemListViewModel, UpdateItemListDto>();

            CreateMap<UpdateItemCompletenessViewModel, UpdateItemCompletenessDto>();

            CreateMap<CreateItemViewModel, CreateItemDto>();

            CreateMap<UpdateItemViewModel, UpdateItemDto>();

            CreateMap<CreateRouteViewModel, CreateRouteDto>();

            CreateMap<UpdateRouteViewModel, UpdateRouteDto>();

            CreateMap<PaginationViewModel, PaginationDto>();

            CreateMap<LoginViewModel, LoginDto>();

            CreateMap<RegisterUserViewModel, RegisterUserDto>();

            CreateMap<UpdateUserViewModel, UpdateUserDto>();

            CreateMap<GrantSubscriberRoleViewModel, GrantSubscriberRoleDto>();

            CreateMap<GrantApplicationRolesViewModel, GrantApplicationRolesDto>();
        }
    }
}
