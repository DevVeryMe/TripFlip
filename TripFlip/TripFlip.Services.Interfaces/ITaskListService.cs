using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TaskListDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Interface that describes supported CRUD operations with Task list entity.
    /// </summary>
    public interface ITaskListService
    {
        /// <summary>
        /// Gets all Task lists with given Route id.
        /// </summary>
        /// <param name="routeId">Route id.</param>
        /// <param name="searchString">String to search in Task lists.</param>
        /// <param name="paginationDto">Pagination settings.</param>
        /// <returns>Paged list of Task list DTOs that represent the database entries.</returns>
        Task<PagedList<TaskListDto>> GetAllByRouteIdAsync(int routeId, 
            string searchString,
            PaginationDto paginationDto);

        /// <summary>
        /// Gets Task list by id.
        /// </summary>
        /// <param name="id">Task list id.</param>
        /// <returns>Task list DTO that represents the database entry with the given id.</returns>
        Task<TaskListDto> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new Task list.
        /// </summary>
        /// <param name="createTaskListDto">Data to create a new Task list.</param>
        /// <returns>Task list DTO that represents the new entry that was added to database.</returns>
        Task<TaskListDto> CreateAsync(CreateTaskListDto createTaskListDto);

        /// <summary>
        /// Updates Task list.
        /// </summary>
        /// <param name="updateTaskListDto">New Task list data with existing Task list id.</param>
        /// <returns>Task list DTO that represents the updated database entry.</returns>
        Task<TaskListDto> UpdateAsync(UpdateTaskListDto updateTaskListDto);

        /// <summary>
        /// Deletes Task list.
        /// </summary>
        /// <param name="id">Task list id.</param>
        Task DeleteByIdAsync(int id);
    }
}
