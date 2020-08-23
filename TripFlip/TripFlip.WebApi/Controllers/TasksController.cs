using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.TaskViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TasksController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new Task.
        /// </summary>
        /// <param name="createTaskViewModel">Task to create.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskViewModel createTaskViewModel)
        {
            var taskDto = _mapper.Map<TaskDto>(createTaskViewModel);

            var taskToReturnDto = await _taskService.CreateAsync(taskDto);

            var createdTaskViewModel = _mapper.Map<GetTaskViewModel>(taskToReturnDto);

            return Ok(createdTaskViewModel);
        }

        /// <summary>
        /// Gets all Tasks from a certain task list.
        /// </summary>
        /// <param name="taskListId">Task list id.</param>
        [HttpGet]
        public async Task<IActionResult> GetAllByTaskListIdAsync([FromQuery] int taskListId)
        {
            var taskDtos = await _taskService.GetAllByTaskListIdAsync(taskListId);
            var taskViewModels = _mapper.Map<List<GetTaskViewModel>>(taskDtos);

            return Ok(taskViewModels);
        }

        /// <summary>
        /// Gets task by id.
        /// </summary>
        /// <param name="id">Task id.</param>
        /// <returns>Task view model.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var taskDto = await _taskService.GetByIdAsync(id);
            var getTaskViewModel = _mapper.Map<GetTaskViewModel>(taskDto);

            return Ok(getTaskViewModel);
        }

        /// <summary>
        /// Updates existing task.
        /// </summary>
        /// <param name="taskViewModel">New task data with existing task id.</param>
        /// <returns>Updated task view model.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTaskViewModel updateTaskViewModel)
        {
            var taskDto = _mapper.Map<UpdateTaskDto>(updateTaskViewModel);

            var updatedTaskDto = await _taskService.UpdateAsync(taskDto);
            var updatedTaskViewModel = _mapper.Map<UpdateTaskViewModel>(updatedTaskDto);

            return Ok(updatedTaskViewModel);
        }

        /// <summary>
        /// Updates existing task priority level.
        /// </summary>
        /// <param name="id">Task id.</param>
        /// <param name="priorityLevel">New task priority level.</param>
        /// <returns>Updated task view model.</returns>
        [HttpPut]
        [Route("change-priority")]
        public async Task<IActionResult> UpdatePriorityAsync(
            [FromBody] UpdateTaskPriorityViewModel updateTaskPriorityViewModel
            )
        {
            var updateTaskPriorityDto = _mapper.Map<UpdateTaskPriorityDto>(updateTaskPriorityViewModel);

            var updatedTaskDto = await _taskService.UpdatePriorityAsync(updateTaskPriorityDto);
            var updatedTaskViewModel = _mapper.Map<UpdateTaskViewModel>(updatedTaskDto);

            return Ok(updatedTaskViewModel);
        }

        /// <summary>
        /// Deletes task by id.
        /// </summary>
        /// <param name="id">Task to delete id.</param>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _taskService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
