using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels.TaskViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/TaskLists/{taskListId}/[controller]")]
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
        [HttpGet]
        public async Task<IActionResult> GetAllByTaskListIdAsync(int taskListId)
        {
            var taskDtos = await _taskService.GetAllByTaskListIdAsync(taskListId);
            var taskViewModels = _mapper.Map<List<GetTaskViewModel>>(taskDtos);

            return Ok(taskViewModels);
        }
    }
}
