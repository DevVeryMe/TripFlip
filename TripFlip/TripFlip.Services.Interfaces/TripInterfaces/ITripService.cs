using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces.TripInterfaces
{
    public interface ITripService
    {
        /// <summary>
        /// Gets all trips.
        /// </summary>
        Task<IEnumerable<TripDto>>GetAllTripsAsync();
    }
}
