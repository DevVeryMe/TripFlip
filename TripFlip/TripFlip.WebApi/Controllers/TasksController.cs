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
        public async Task<IActionResult> CreateAsync(CreateTaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                var taskDto = _mapper.Map<TaskDto>(taskViewModel);
                await _taskService.CreateTaskAsync(taskDto);

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
            var tasks = await _taskService.GetAllTasksAsync();
            var taskViewModels = _mapper.Map<List<FullTaskViewModel>>(tasks);

            return Ok(taskViewModels);
        }
    }
}
