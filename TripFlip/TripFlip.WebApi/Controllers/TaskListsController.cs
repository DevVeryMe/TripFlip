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
    }
}
