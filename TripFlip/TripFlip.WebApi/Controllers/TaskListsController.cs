using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TaskListDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.ViewModels;
using TripFlip.ViewModels.TaskListViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/task-lists")]
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
        /// Creates new Task list.
        /// </summary>
        /// <param name="createTaskListViewModel">Data to create Task list.</param>
        /// <returns>Task list view model that
        /// represents the new entry that was added to database.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /task-lists
        ///     {
        ///         "title": "New task list.",
        ///         "routeId": 1
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(GetTaskListViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskListViewModel createTaskListViewModel)
        {
            var taskListDto = _mapper.Map<CreateTaskListDto>(createTaskListViewModel);
            var createdTaskListDto = await _taskListService.CreateAsync(taskListDto);

            var createdTaskListViewModel = _mapper.Map<GetTaskListViewModel>(createdTaskListDto);

            return Ok(createdTaskListViewModel);
        }

        /// <summary>
        /// Gets all Task lists with the given Route id.
        /// </summary>
        /// <param name="routeId">Route id.</param>
        /// <param name="searchString">String to filter Task lists.</param>
        /// <param name="paginationViewModel">Pagination settings.</param>
        /// <returns>Paged list of Task list view models that
        /// represent database entries specified by Route id.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<GetTaskListViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByRouteIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromQuery] int routeId,
            [FromQuery] string searchString,
            [FromQuery] PaginationViewModel paginationViewModel)
        {
            var paginationDto = _mapper.Map<PaginationDto>(paginationViewModel);

            var taskListDtos = await _taskListService.GetAllByRouteIdAsync(routeId,
                searchString, paginationDto);
            var taskListViewModels = _mapper.Map< PagedList<GetTaskListViewModel> >(taskListDtos);

            return Ok(taskListViewModels);
        }

        /// <summary>
        /// Gets Task list by id.
        /// </summary>
        /// <param name="id">Task list id.</param>
        /// <returns>Task list view model with specified id that
        /// represents database entry.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetTaskListViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            var taskListDto = await _taskListService.GetByIdAsync(id);
            var taskListViewModel = _mapper.Map<GetTaskListViewModel>(taskListDto);

            return Ok(taskListViewModel);
        }

        /// <summary>
        /// Updates existing Task list.
        /// </summary>
        /// <param name="updateTaskListViewModel">New Task list data with existing Task list id.</param>
        /// <returns>Task list view model
        /// that represents the updated database entry.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /task-lists
        ///     {
        ///         "id": 1,
        ///         "title": "Updated task list."
        ///     }
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(UpdateTaskListViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTaskListViewModel updateTaskListViewModel)
        {
            var taskListDto = _mapper.Map<UpdateTaskListDto>(updateTaskListViewModel);

            var updatedTaskListDto = await _taskListService.UpdateAsync(taskListDto);
            var updatedTaskListViewModel = _mapper.Map<UpdateTaskListViewModel>(updatedTaskListDto);

            return Ok(updatedTaskListViewModel);
        }

        /// <summary>
        /// Deletes Task list.
        /// </summary>
        /// <param name="id">Task list id.</param>
        /// <returns>No content (HTTP code 204).</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
            [FromRoute] int id)
        {
            await _taskListService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
