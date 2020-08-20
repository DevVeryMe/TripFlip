﻿using AutoMapper;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.DTO.TaskListDtos;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.Services.DTO.ItemListDtos;
using TripFlip.Services.DTO.RouteDtos;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.ViewModels.TaskViewModels;
using TripFlip.ViewModels.TaskListViewModels;
using TripFlip.ViewModels.RouteViewModels;
using TripFlip.ViewModels.ItemViewModels;
using TripFlip.ViewModels.ItemListViewModels;

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

            CreateMap<ItemDto, ItemViewModel>();

            CreateMap<ResultRouteDto, ResultRouteViewModel>();

            CreateMap<ResultItemListDto, ResultItemListViewModel>();
        }
    }
}
