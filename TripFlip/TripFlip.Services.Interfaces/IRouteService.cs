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
    }
}
