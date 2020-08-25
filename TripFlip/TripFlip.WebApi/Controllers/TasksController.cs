using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels.Enums;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.ViewModels;
using TripFlip.ViewModels.TaskViewModels;
using Microsoft.AspNetCore.Http;

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
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /tasks
        ///     {
        ///         "description": "Some task description.",
        ///         "priorityLevel": 1,                         /*1 - Low, 2 - Normal, 3 - High, 4 - Urgent*/
        ///         "taskListId": 1
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(GetTaskViewModel), StatusCodes.Status200OK)]
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
        /// <param name="searchString">String to find tasks.</param>
        /// <param name="paginationViewModel">Pagination settings.</param>
        /// <returns>>Paged list of Task view models.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<GetTaskViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByTaskListIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromQuery] int taskListId,
            [FromQuery] string searchString,
            [FromQuery] PaginationViewModel paginationViewModel)
        {
            var paginationDto = _mapper.Map<PaginationDto>(paginationViewModel);

            var taskDtos = await _taskService.
                GetAllByTaskListIdAsync(taskListId, searchString, paginationDto);

            var taskViewModels = _mapper.Map< PagedList<GetTaskViewModel> >(taskDtos);

            return Ok(taskViewModels);
        }

        /// <summary>
        /// Gets task by id.
        /// </summary>
        /// <param name="id">Task id.</param>
        /// <returns>Task view model.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetTaskViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            var taskDto = await _taskService.GetByIdAsync(id);
            var getTaskViewModel = _mapper.Map<GetTaskViewModel>(taskDto);

            return Ok(getTaskViewModel);
        }

        /// <summary>
        /// Updates existing task.
        /// </summary>
        /// <param name="updateTaskViewModel">New task data with existing task id.</param>
        /// <returns>Updated task view model.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /tasks
        ///     {
        ///         "id": 1,
        ///         "description": "Some task description.",
        ///         "priorityLevel": 1,                         /*1 - Low, 2 - Normal, 3 - High, 4 - Urgent*/
        ///         "taskListId": 1
        ///     }
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(UpdateTaskViewModel), StatusCodes.Status200OK)]
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
        /// <param name="updateTaskPriorityViewModel">New task priority level.</param>
        /// <returns>Updated task view model.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /tasks/change-priority
        ///     {
        ///         "id": 1,
        ///         "priorityLevel": 1      /*1 - Low, 2 - Normal, 3 - High, 4 - Urgent*/
        ///     }
        /// </remarks>
        [HttpPut("change-priority")]
        [ProducesResponseType(typeof(UpdateTaskViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePriorityAsync(
            [FromBody] UpdateTaskPriorityViewModel updateTaskPriorityViewModel)
        {
            var updateTaskPriorityDto = _mapper.Map<UpdateTaskPriorityDto>(updateTaskPriorityViewModel);

            var updatedTaskDto = await _taskService.UpdatePriorityAsync(updateTaskPriorityDto);
            var updatedTaskViewModel = _mapper.Map<UpdateTaskViewModel>(updatedTaskDto);

            return Ok(updatedTaskViewModel);
        }

        /// <summary>
        /// Updates existing task's completeness.
        /// </summary>
        /// <param name="updateTaskCompletenessViewModel"> Object with task id and bool field
        /// that determines is task completed.</param>
        /// <returns>Updated task view model.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /tasks/change-completeness
        ///     {
        ///         "id": 1,
        ///         "isCompleted": true
        ///     }
        /// </remarks>
        [HttpPut("change-completeness")]
        [ProducesResponseType(typeof(UpdateTaskViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCompletenessAsync(
            [FromBody] UpdateTaskCompletenessViewModel updateTaskCompletenessViewModel)
        {
            var updateTaskCompletenessDto = 
                _mapper.Map<UpdateTaskCompletenessDto>(updateTaskCompletenessViewModel);

            var updatedTaskDto = await _taskService.UpdateCompletenessAsync(updateTaskCompletenessDto);
            var updatedTaskViewModel = _mapper.Map<UpdateTaskViewModel>(updatedTaskDto);

            return Ok(updatedTaskViewModel);
        }

        /// <summary>
        /// Deletes task by id.
        /// </summary>
        /// <param name="id">Task to delete id.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            await _taskService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
