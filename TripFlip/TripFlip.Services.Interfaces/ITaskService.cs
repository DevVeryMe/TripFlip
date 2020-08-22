﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Gets all tasks from certain task list.
        /// </summary>
        /// <param name="taskListId">Task list id.</param>
        /// <param name="paginationDto">Pagination settings.</param>
        /// <returns>Paged list with Task DTOs.</returns>
        Task<PagedList<TaskDto>> GetAllByTaskListIdAsync(int taskListId, PaginationDto paginationDto);

        /// <summary>
        /// Gets task by id.
        /// </summary>
        /// <param name="id">Task id.</param>
        /// <returns>Task DTO.</returns>
        Task<TaskDto> GetByIdAsync(int id);

        /// <summary>
        /// Creates new task.
        /// </summary>
        /// <param name="taskDto">Task data.</param>
        /// <returns>Created task.</returns>
        Task<TaskDto> CreateAsync(TaskDto taskDto);

        /// <summary>
        /// Updates existing task.
        /// </summary>
        /// <param name="taskDto">New task data.</param>
        /// <returns>Updated task DTO.</returns>
        Task<TaskDto> UpdateAsync(UpdateTaskDto taskDto);

        /// <summary>
        /// Deletes task by id.
        /// </summary>
        /// <param name="id">Task to delete id.</param>
        Task DeleteByIdAsync(int id);
    }
}
