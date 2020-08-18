using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels.TaskViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        /// <param name="taskViewModel">task to create</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskViewModel createTaskViewModel, int taskListId)
        {
            var taskDto = _mapper.Map<TaskDto>(createTaskViewModel);
            taskDto.TaskListId = taskListId;

            var taskToReturnDto = await _taskService.CreateAsync(taskDto);

            var createdTaskViewModel = _mapper.Map<GetTaskViewModel>(taskToReturnDto);

            return Ok(createdTaskViewModel);
        }

        /// <summary>
        /// Gets all Tasks from a certain task list.
        /// </summary>
        /// <param name="taskListId">task list id</param>
        [HttpGet]
        public async Task<IActionResult> GetAllByTaskListIdAsync(int taskListId)
        {
            var taskDtos = await _taskService.GetAllByTaskListIdAsync(taskListId);
            var taskViewModels = _mapper.Map<List<GetTaskViewModel>>(taskDtos);

            return Ok(taskViewModels);
        }

        /// <summary>
        /// Gets task by id.
        /// </summary>
        /// <param name="id">task id</param>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var taskDto = await _taskService.GetByIdAsync(id);
            var getTaskViewModel = _mapper.Map<GetTaskViewModel>(taskDto);

            return Ok(getTaskViewModel);
        }

        /// <summary>
        /// Updates existing task.
        /// </summary>
        /// <param name="taskViewModel">new task data with existing task id</param>
        /// <returns>updated task</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskViewModel updateTaskViewModel)
        {
            var taskDto = _mapper.Map<TaskDto>(updateTaskViewModel);

            var updatedTaskDto = await _taskService.UpdateAsync(taskDto);
            var updatedTaskViewModel = _mapper.Map<UpdateTaskViewModel>(updatedTaskDto);

            return Ok(updatedTaskViewModel);
        }

        /// <summary>
        /// Deletes task by id.
        /// </summary>
        /// <param name="id">task to delete id</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
