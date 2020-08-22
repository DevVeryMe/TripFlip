using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.RouteDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Interface that describes supported CRUD operations with Route entity.
    /// </summary>
    public interface IRouteService
    {
        /// <summary>
        /// Creates new Route with the given <see cref="CreateRouteDto"/> object. Applies changes to database asynchronously.
        /// </summary>
        /// <returns><see cref="ResultRouteDto"/> object that represents the new entry that was added to database.</returns>
        Task<ResultRouteDto> CreateAsync(CreateRouteDto createRouteDto);

        /// <summary>
        /// Updates the Route with the given <see cref="UpdateRouteDto"/> object. Applies changes to database asynchronously.
        /// </summary>
        /// <returns><see cref="ResultRouteDto"/> bject that represents the updated entry in database.</returns>
        Task<ResultRouteDto> UpdateAsync(UpdateRouteDto updateRouteDto);

        /// <summary>
        /// Returns Route with the given Id.
        /// </summary>
        /// <returns>Object that represents the database entry with the given Id.</returns>
        Task<ResultRouteDto> GetByIdAsync(int routeId);

        /// <summary>
        /// Returns page of Routes with the given Trip Id.
        /// </summary>
        /// <returns><returns>Object that represents the <see cref="PagedList{ResultRouteDto}"/> collection of database entries with the given Trip Id.</returns>
        Task<PagedList<ResultRouteDto>> GetAllByTripIdAsync(int tripId, BasicPaginationFilter paginationFilter);

        /// <summary>
        /// Deletes Route with the given Id.
        /// </summary>
        Task DeleteAsync(int id);
    }
}
