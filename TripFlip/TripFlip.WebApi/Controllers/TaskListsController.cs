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
        /// Creates new TaskList.
        /// </summary>
        /// <param name="taskListViewModel">TaskList to create</param>
        /// <returns>created TaskList view model</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskListViewModel taskListViewModel)
        {
            var taskListDto = _mapper.Map<TaskListDto>(taskListViewModel);
            var taskDtoToReturn = await _taskListService.CreateAsync(taskListDto);

            var taskListViewModelToReturn = _mapper.Map<GetTaskListViewModel>(taskDtoToReturn);

            return Ok(taskListViewModelToReturn);
        }

        /// <summary>
        /// Gets TaskList by id.
        /// </summary>
        /// <param name="id">task list id</param>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var taskList = await _taskListService.GetByIdAsync(id);
            var taskListViewModel = _mapper.Map<GetTaskListViewModel>(taskList);

            return Ok(taskListViewModel);
        }
    }
}
