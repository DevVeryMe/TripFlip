using AutoMapper;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Dto.RoutePointDtos;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Dto.TaskListDtos;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Dto.TripRoleDtos;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.ViewModels.ItemListViewModels;
using TripFlip.ViewModels.ItemViewModels;
using TripFlip.ViewModels.RoutePointViewModels;
using TripFlip.ViewModels.RouteViewModels;
using TripFlip.ViewModels.TaskListViewModels;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.ViewModels.TripRoleViewModels;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.ViewModels.UserViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelFromDto : Profile
    {
        public ViewModelFromDto()
        {
            CreateMap<TripDto, TripViewModel>();

            CreateMap<PagedList<TripDto>, PagedList<TripViewModel>>();

            CreateMap<TaskDto, TaskViewModel>();

            CreateMap<TaskDto, UpdateTaskViewModel>();

            CreateMap<TaskListDto, TaskListViewModel>();

            CreateMap<TaskListDto, UpdateTaskListViewModel>();

            CreateMap<ItemDto, ItemViewModel>();

            CreateMap<RouteDto, RouteViewModel>();

            CreateMap<PagedList<RouteDto>, PagedList<RouteViewModel>>();

            CreateMap<ItemListDto, ItemListViewModel>();

            CreateMap<PagedList<ItemDto>, PagedList<ItemViewModel>>();

            CreateMap<PagedList<ItemListDto>, PagedList<ItemListViewModel>>();

            CreateMap<PagedList<TaskDto>, PagedList<TaskViewModel>>();

            CreateMap<PagedList<TaskListDto>, PagedList<TaskListViewModel>>();

            CreateMap<UserDto, UserViewModel>();

            CreateMap<AuthenticatedUserDto, AuthenticatedUserViewModel>();

            CreateMap<PagedList<UserDto>, PagedList<UserViewModel>>();

            CreateMap<ItemListWithItemsDto, ItemListWithItemsViewModel>();

            CreateMap<TaskListWithTasksDto, TaskListWithTasksViewModel>();

            CreateMap<RoutePointDto, RoutePointViewModel>();

            CreateMap<RouteWithPointsItemAndTaskListsDto, RouteWithPointsItemAndTaskListsViewModel>();

            CreateMap<TripRoleDto, TripRoleViewModel>();

            CreateMap<TripWithRoutesAndUserRolesDto, TripWithRoutesAndUserRolesViewModel>();

            CreateMap<ItemWithAssigneesDto, ItemWithoutListIdViewModel>();

            CreateMap<TaskWithAssigneesDto, TaskWithoutListIdViewModel>();

            CreateMap<UsersByTripAndCategorizedByRoleDto, UsersByTripAndCategorizedByRoleViewModel>();
        }
    }
}
