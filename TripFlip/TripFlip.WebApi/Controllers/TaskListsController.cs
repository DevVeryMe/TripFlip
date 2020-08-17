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
        /// <param name="taskListViewModel">task list to create</param>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskListViewModel taskListViewModel)
        {
            var taskListDto = _mapper.Map<TaskListDto>(taskListViewModel);
            var taskDtoToReturn = await _taskListService.CreateAsync(taskListDto);

            var taskListViewModelToReturn = _mapper.Map<GetTaskListViewModel>(taskDtoToReturn);

            return Ok(taskListViewModelToReturn);
        }
    }
}
