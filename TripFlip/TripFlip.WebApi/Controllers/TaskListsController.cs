using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        /// Creates new task list.
        /// </summary>
        /// <param name="createTaskListViewModel">Task list to create.</param>
        /// <returns>Created task list view model.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskListViewModel createTaskListViewModel)
        {
            var taskListDto = _mapper.Map<CreateTaskListDto>(createTaskListViewModel);
            var createdTaskListDto = await _taskListService.CreateAsync(taskListDto);

            var createdTaskListViewModel = _mapper.Map<GetTaskListViewModel>(createdTaskListDto);

            return Ok(createdTaskListViewModel);
        }

        /// <summary>
        /// Gets task list by id.
        /// </summary>
        /// <param name="id">Task list id.</param>
        /// <returns>Task list view model with specified id.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var taskListDto = await _taskListService.GetByIdAsync(id);
            var taskListViewModel = _mapper.Map<GetTaskListViewModel>(taskListDto);

            return Ok(taskListViewModel);
        }

        /// <summary>
        /// Updates existing TaskList.
        /// </summary>
        /// <param name="updateTaskListViewModel">New task list data with existing task list id.</param>
        /// <returns>Updated task list view model.</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskListViewModel updateTaskListViewModel)
        {
            var taskListDto = _mapper.Map<UpdateTaskListDto>(updateTaskListViewModel);

            var updatedTaskListDto = await _taskListService.UpdateAsync(taskListDto);
            var updatedTaskListViewModel = _mapper.Map<UpdateTaskListViewModel>(updatedTaskListDto);

            return Ok(updatedTaskListViewModel);
        }

        /// <summary>
        /// Deletes task list by id.
        /// </summary>
        /// <param name="id">Task list to delete id.</param>
        /// <returns>No content.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskListService.DeleteAsync(id);

            return NoContent();
        }
    }
}
