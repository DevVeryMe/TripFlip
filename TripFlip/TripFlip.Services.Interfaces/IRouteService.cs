using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Describes supported operations with Route entity.
    /// </summary>
    public interface IRouteService
    {
        /// <summary>
        /// Creates a new Route.
        /// </summary>
        /// <param name="createRouteDto">Data to create a new Route.</param>
        /// <returns>Route DTO that represents the new entry that was added to database.</returns>
        Task<ResultRouteDto> CreateAsync(CreateRouteDto createRouteDto);

        /// <summary>
        /// Updates the Route.
        /// </summary>
        /// <param name="updateRouteDto">New Route data with existing Route id.</param>
        /// <returns>Route DTO that represents the updated database entry.</returns>
        Task<ResultRouteDto> UpdateAsync(UpdateRouteDto updateRouteDto);

        /// <summary>
        /// Gets Route with the given id.
        /// </summary>
        /// <param name="id">Route id.</param>
        /// <returns>Route DTO that represents the database entry with the given id.</returns>
        Task<ResultRouteDto> GetByIdAsync(int id);

        /// <summary>
        /// Gets all Routes with the given Trip id.
        /// </summary>
        /// <param name="tripId">Trip id.</param>
        /// <param name="paginationDto">Pagination parameters.</param>
        /// <param name="searchString">String to filter Routes.</param>
        /// <returns>Paged list of Route DTOs that represent the database entries.</returns>
        Task<PagedList<ResultRouteDto>> GetAllByTripIdAsync(int tripId,
            string searchString,
            PaginationDto paginationDto);

        /// <summary>
        /// Deletes Route.
        /// </summary>
        /// <param name="id">Route id.</param>
        Task DeleteByIdAsync(int id);
    }
}
