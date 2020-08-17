using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO.TaskDtos;
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
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskViewModel taskViewModel)
        {
            var taskDto = _mapper.Map<TaskDto>(taskViewModel);
            var taskDtoToReturn = await _taskService.CreateAsync(taskDto);

            var taskViewModelToReturn = _mapper.Map<GetTaskViewModel>(taskDtoToReturn);

            return Ok(taskViewModelToReturn);
        }

        /// <summary>
        /// Gets all Tasks.
        /// </summary>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var tasks = await _taskService.GetAllAsync();
            var taskViewModels = _mapper.Map<List<GetTaskViewModel>>(tasks);

            return Ok(taskViewModels);
        }

        /// <summary>
        /// Gets task by id.
        /// </summary>
        /// <param name="id">task id</param>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var task = await _taskService.GetAsync(id);
            var taskViewModel = _mapper.Map<GetTaskViewModel>(task);

            return Ok(taskViewModel);
        }

        /// <summary>
        /// Updates existing task.
        /// </summary>
        /// <param name="taskViewModel">new task data with existing task id</param>
        /// <param name="id">task to update id</param>
        /// <returns>updated task</returns>
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskViewModel taskViewModel)
        {
            var taskDto = _mapper.Map<TaskDto>(taskViewModel);

            taskDto = await _taskService.UpdateAsync(taskDto);
            taskViewModel = _mapper.Map<UpdateTaskViewModel>(taskDto);

            return Ok(taskViewModel);
        }

        /// <summary>
        /// Deletes task by id.
        /// </summary>
        /// <param name="id">task to delete id</param>
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.DeleteAsync(id);

            return Ok();
        }
    }
}
