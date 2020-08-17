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
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskViewModel taskViewModel)
        {
            var taskDto = _mapper.Map<TaskDto>(taskViewModel);
            var taskToReturnDto = await _taskService.CreateAsync(taskDto);

            var taskToreturnViewModel = _mapper.Map<GetTaskViewModel>(taskToReturnDto);

            return Ok(taskToreturnViewModel);
        }

        /// <summary>
        /// Gets all Tasks from certain task list.
        /// </summary>
        [HttpGet]
        [Route("/api/TaskLists/{taskListId}/Tasks")]
        public async Task<IActionResult> GetAsync(int taskListId)
        {
            var taskDtos = await _taskService.GetAllByTaskListIdAsync(taskListId);
            var taskViewModels = _mapper.Map<List<GetTaskViewModel>>(taskDtos);

            return Ok(taskViewModels);
        }
    }
}
