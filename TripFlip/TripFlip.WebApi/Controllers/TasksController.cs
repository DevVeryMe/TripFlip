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
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskViewModel createTaskViewModel)
        {
            var taskDto = _mapper.Map<TaskDto>(createTaskViewModel);
            var taskToReturnDto = await _taskService.CreateAsync(taskDto);

            var createdTaskViewModel = _mapper.Map<GetTaskViewModel>(taskToReturnDto);

            return Ok(createdTaskViewModel);
        }
    }
}
