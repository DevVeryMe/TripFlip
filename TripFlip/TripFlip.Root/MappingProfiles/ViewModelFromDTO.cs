﻿using AutoMapper;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.DTO.TaskListDtos;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.ViewModels.ItemViewModels;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.ViewModels.TaskListViewModels;

namespace TripFlip.Root.MappingProfiles
{
    public class ViewModelFromDto : Profile
    {
        public ViewModelFromDto()
        {
            CreateMap<TripDto, TripViewModel>();

            CreateMap<TaskDto, GetTaskViewModel>();

            CreateMap<TaskDto, UpdateTaskViewModel>();

            CreateMap<TaskListDto, GetTaskListViewModel>();

            CreateMap<TaskListDto, UpdateTaskListViewModel>();

            CreateMap<ItemDto, ItemViewModel>();
        }
    }
}
