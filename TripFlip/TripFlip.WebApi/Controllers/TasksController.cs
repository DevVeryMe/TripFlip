using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.ViewModels;
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
        /// Creates Task.
        /// </summary>
        /// <param name="createTaskViewModel">Data to create Task.</param>
        /// <returns>Task view model that
        /// represents the new entry that was added to database.</returns>
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
        [ProducesResponseType(typeof(TaskViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskViewModel createTaskViewModel)
        {
            var createTaskDto = _mapper.Map<CreateTaskDto>(createTaskViewModel);

            var createdTaskDto = await _taskService.CreateAsync(createTaskDto);

            var createdTaskViewModel = _mapper.Map<TaskViewModel>(createdTaskDto);

            return Ok(createdTaskViewModel);
        }

        /// <summary>
        /// Gets all Tasks with the given Task list id.
        /// </summary>
        /// <param name="taskListId">Task list id.</param>
        /// <param name="searchString">String to filter Tasks.</param>
        /// <param name="paginationViewModel">Pagination settings.</param>
        /// <returns>Paged list of Task view models that
        /// represent database entries specified by Task list id.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<TaskViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByTaskListIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromQuery] int taskListId,
            [FromQuery] string searchString,
            [FromQuery] PaginationViewModel paginationViewModel)
        {
            var paginationDto = _mapper.Map<PaginationDto>(paginationViewModel);

            var pagedTaskDtos = await _taskService.
                GetAllByTaskListIdAsync(taskListId, searchString, paginationDto);

            var pagedTaskViewModels = _mapper.Map< PagedList<TaskViewModel> >(pagedTaskDtos);

            return Ok(pagedTaskViewModels);
        }

        /// <summary>
        /// Gets Task by id.
        /// </summary>
        /// <param name="id">Task id.</param>
        /// <returns>Task view model with specified id that
        /// represents database entry.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            var taskDto = await _taskService.GetByIdAsync(id);
            var taskViewModel = _mapper.Map<TaskViewModel>(taskDto);

            return Ok(taskViewModel);
        }

        /// <summary>
        /// Updates existing Task.
        /// </summary>
        /// <param name="updateTaskViewModel">New Task data with existing Task id.</param>
        /// <returns>Task view model
        /// that represents the updated database entry.</returns>
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
            var updateTaskDto = _mapper.Map<UpdateTaskDto>(updateTaskViewModel);

            var updatedTaskDto = await _taskService.UpdateAsync(updateTaskDto);
            var updatedTaskViewModel = _mapper.Map<UpdateTaskViewModel>(updatedTaskDto);

            return Ok(updatedTaskViewModel);
        }

        /// <summary>
        /// Updates existing Task's priority level.
        /// </summary>
        /// <param name="updateTaskPriorityViewModel">New Task's priority level.</param>
        /// <returns>Task view model
        /// that represents the updated database entry.</returns>
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
        /// Updates existing Task's completeness.
        /// </summary>
        /// <param name="updateTaskCompletenessViewModel">Data with Task id and field
        /// that determines whether task is completed.</param>
        /// <returns>Task view model
        /// that represents the updated database entry.</returns>
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
        /// Deletes Task.
        /// </summary>
        /// <param name="id">Task to delete id.</param>
        /// <returns>No content (HTTP code 204).</returns>
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
