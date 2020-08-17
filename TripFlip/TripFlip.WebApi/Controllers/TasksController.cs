using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var taskViewModels = _mapper.Map<List<FullTaskViewModel>>(tasks);

            return Ok(taskViewModels);
        }

        /// <summary>
        /// Updates existing task.
        /// </summary>
        /// <param name="taskViewModel">new task data</param>
        /// <param name="id">task to update id</param>
        /// <returns>updated task</returns>
        [HttpPut]
        [Route("{id}/update")]
        public async Task<IActionResult> Update(int id, [FromBody] FullTaskViewModel taskViewModel)
        {
            var taskDto = _mapper.Map<TaskDto>(taskViewModel);
            taskDto.Id = id;

            taskDto = await _taskService.UpdateAsync(taskDto);
            taskViewModel = _mapper.Map<FullTaskViewModel>(taskDto);

            return Ok(taskViewModel);
        }
    }
}
