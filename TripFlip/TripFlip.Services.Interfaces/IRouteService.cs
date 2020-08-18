using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Interface that describes supported CRUD operations with Route entity
    /// </summary>
    public interface IRouteService
    {
        /// <summary>
        /// Creates new Route with the given <see cref="RouteDto"/> object. Applies changes to database asynchronously.
        /// </summary>
        /// <returns><see cref="RouteDto"/> object that represents the new entry that was added to database</returns>
        Task<RouteDto> CreateAsync(RouteDto routeDto);

        /// <summary>
        /// Updates the Route with the given <see cref="RouteDto"/> object. Applies changes to database asynchronously.
        /// </summary>
        /// <returns>object that represents the updated entry in database</returns>
        Task<RouteDto> UpdateAsync(RouteDto routeDto);

        /// <summary>
        /// Returns Route with the given Id. Makes asynchronous database query.
        /// </summary>
        /// <returns>object that represents the database entry with the given Id</returns>
        Task<RouteDto> GetAsync(int routeId);

        /// <summary>
        /// Returns all Routes with the given Trip Id. Makes asynchronous database query.
        /// </summary>
        /// <returns>object that represents the list of database entries with the given Trip Id</returns>
        Task<List<RouteDto>> GetAllByTripAsync(int tripId);
    }
}
