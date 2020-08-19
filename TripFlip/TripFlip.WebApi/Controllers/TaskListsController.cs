﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO.TaskListDtos;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels.TaskListViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListsController : ControllerBase
    {
        private readonly ITaskListService _taskListService;
        private readonly IMapper _mapper;

        public TaskListsController(ITaskListService taskListService, IMapper mapper)
        {
            _taskListService = taskListService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new TaskList.
        /// </summary>
        /// <param name="createTaskListViewModel">TaskList to create</param>
        /// <returns>created TaskList view model</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskListViewModel createTaskListViewModel)
        {
            var taskListDto = _mapper.Map<TaskListDto>(createTaskListViewModel);
            var createdTaskDto = await _taskListService.CreateAsync(taskListDto);

            var createdTaskListViewModel = _mapper.Map<GetTaskListViewModel>(createdTaskDto);

            return Ok(createdTaskListViewModel);
        }

        /// <summary>
        /// Gets all task lists from a certain route.
        /// </summary>
        /// <param name="routeId">route id</param>
        [HttpGet]
        public async Task<IActionResult> GetAllByRouteIdAsync([FromQuery] int routeId)
        {
            var taskDtos = await _taskListService.GetAllByRouteIdAsync(routeId);
            var taskViewModels = _mapper.Map<List<GetTaskListViewModel>>(taskDtos);

            return Ok(taskViewModels);
        }

        /// <summary>
        /// Gets TaskList by id.
        /// </summary>
        /// <param name="id">task list id</param>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var taskListDto = await _taskListService.GetByIdAsync(id);
            var taskListViewModel = _mapper.Map<GetTaskListViewModel>(taskListDto);

            return Ok(taskListViewModel);
        }

        /// <summary>
        /// Updates existing TaskList.
        /// </summary>
        /// <param name="updateTaskListViewModel">new TaskList data with existing TaskList id</param>
        /// <returns>updated TaskList</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskListViewModel updateTaskListViewModel)
        {
            var taskListDto = _mapper.Map<TaskListDto>(updateTaskListViewModel);

            var updatedTaskListDto = await _taskListService.UpdateAsync(taskListDto);
            var updatedTaskListViewModel = _mapper.Map<UpdateTaskListViewModel>(updatedTaskListDto);

            return Ok(updatedTaskListViewModel);
        }

        /// <summary>
        /// Deletes TaskList by id.
        /// </summary>
        /// <param name="id">TaskList to delete id</param>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _taskListService.DeleteAsync(id);

            return NoContent();
        }
    }
}
