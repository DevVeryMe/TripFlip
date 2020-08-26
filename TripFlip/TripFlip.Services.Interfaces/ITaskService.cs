using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Describes supported operations with Task entity.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Gets all Tasks with given Task list id.
        /// </summary>
        /// <param name="taskListId">Task list id.</param>
        /// <param name="searchString">String to filter Tasks.</param>
        /// <param name="paginationDto">Pagination settings.</param>
        /// <returns>Paged list of Task DTOs that represent the database entries.</returns>
        Task<PagedList<TaskDto>> GetAllByTaskListIdAsync(int taskListId, 
            string searchString,
            PaginationDto paginationDto);

        /// <summary>
        /// Gets Task by id.
        /// </summary>
        /// <param name="id">Task id.</param>
        /// <returns>Task DTO that represents the database entry.</returns>
        Task<TaskDto> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new Task.
        /// </summary>
        /// <param name="createTaskDto">Data to create a new Task.</param>
        /// <returns>Task DTO that represents the new entry that was added to database.</returns>
        Task<TaskDto> CreateAsync(CreateTaskDto createTaskDto);

        /// <summary>
        /// Updates Task.
        /// </summary>
        /// <param name="updateTaskDto">New Task data with existing Task id.</param>
        /// <returns>Task DTO that represents the updated database entry.</returns>
        Task<TaskDto> UpdateAsync(UpdateTaskDto updateTaskDto);

        /// <summary>
        /// Updates existing Task's priority level.
        /// </summary>
        /// <param name="updateTaskPriorityDto">New Task data with id and priority level.</param>
        /// <returns>Task DTO that represents the updated database entry.</returns>
        Task<TaskDto> UpdatePriorityAsync(UpdateTaskPriorityDto updateTaskPriorityDto);

        /// <summary>
        /// Updates existing Task's completeness.
        /// </summary>
        /// <param name="updateTaskCompletenessDto">New Task data with id and completeness field.</param>
        /// <returns>Task DTO that represents the updated database entry.</returns>
        Task<TaskDto> UpdateCompletenessAsync(UpdateTaskCompletenessDto updateTaskCompletenessDto);

        /// <summary>
        /// Deletes Task by id.
        /// </summary>
        /// <param name="id">Task id.</param>
        Task DeleteByIdAsync(int id);
    }
}
