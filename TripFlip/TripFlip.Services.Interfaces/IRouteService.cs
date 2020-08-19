using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Interface that describes supported CRUD operations with Route entity.
    /// </summary>
    public interface IRouteService
    {
        /// <summary>
        /// Creates new Route with the given <see cref="RouteDto"/> object. Applies changes to database asynchronously.
        /// </summary>
        /// <returns><see cref="RouteDto"/> object that represents the new entry that was added to database.</returns>
        Task<RouteDto> CreateAsync(RouteDto routeDto);

        /// <summary>
        /// Updates the Route with the given <see cref="RouteDto"/> object. Applies changes to database asynchronously.
        /// </summary>
        /// <returns>Object that represents the updated entry in database.</returns>
        Task<RouteDto> UpdateAsync(RouteDto routeDto);

        /// <summary>
        /// Returns Route with the given Id.
        /// </summary>
        /// <returns>Object that represents the database entry with the given Id.</returns>
        Task<RouteDto> GetByIdAsync(int routeId);

        /// <summary>
        /// Returns all Routes with the given Trip Id.
        /// </summary>
        /// <returns>Object that represents the <see cref="IEnumerable{RouteDto}"/> collection of database entries with the given Trip Id.</returns>
        Task<IEnumerable<RouteDto>> GetAllByTripIdAsync(int tripId);

        /// <summary>
        /// Deletes Route with the given Id.
        /// </summary>
        Task DeleteAsync(int id);
    }
}
