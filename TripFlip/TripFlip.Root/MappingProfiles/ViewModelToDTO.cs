using AutoMapper;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.ViewModels.ItemViewModels;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.Services.DTO.TaskListDtos;
using TripFlip.ViewModels.TaskListViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<TripViewModel, TripDto>();

            CreateMap<CreateTripViewModel, CreateTripDto>();

            CreateMap<CreateTaskViewModel, TaskDto>();

            CreateMap<UpdateTaskViewModel, TaskDto>();

            CreateMap<CreateTaskListViewModel, TaskListDto>();

            CreateMap<UpdateTaskListViewModel, TaskListDto>();

            CreateMap<CreateItemViewModel, CreateItemDto>();
        }
    }
}
