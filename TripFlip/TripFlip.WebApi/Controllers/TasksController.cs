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
        [Route("/create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                var taskDto = _mapper.Map<TaskDto>(taskViewModel);
                await _taskService.CreateAsync(taskDto);

                return Ok(taskViewModel);
            }

            return Ok();
        }

        /// <summary>
        /// Gets all Tasks.
        /// </summary>
        [HttpGet]
        [Route("/all")]
        public async Task<IActionResult> GetAsync()
        {
            var tasks = await _taskService.GetAllAsync();
            var taskViewModels = _mapper.Map<List<GetTaskViewModel>>(tasks);

            return Ok(taskViewModels);
        }

        /// <summary>
        /// Updates existing task.
        /// </summary>
        /// <param name="taskViewModel">new task data with existing task id</param>
        /// <param name="id">task to update id</param>
        /// <returns>updated task</returns>
        [HttpPut]
        [Route("{/update")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                var taskDto = _mapper.Map<TaskDto>(taskViewModel);

                taskDto = await _taskService.UpdateAsync(taskDto);
                taskViewModel = _mapper.Map<UpdateTaskViewModel>(taskDto);

                return Ok(taskViewModel);
            }

            return Ok();
        }
    }
}
