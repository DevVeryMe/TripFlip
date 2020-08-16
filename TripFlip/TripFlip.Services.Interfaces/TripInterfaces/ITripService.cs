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

        /// <summary>
        /// Gets trip by id.
        /// </summary>
        /// <param name="id">trip id</param>

        /// <summary>
        /// Creates new trip.
        /// </summary>
        /// <param name="tripDto">trip data</param>
        void CreateTrip(TripDto tripDto);

        /// <summary>
        /// Updates existing trip.
        /// </summary>
        /// <param name="tripDto">new trip data</param>
        void UpdateTrip(TripDto tripDto);

        /// <summary>
        /// Deletes trip.
        /// </summary>
        /// <param name="tripDto">trip to delete</param>
        void DeleteTrip(TripDto tripDto);
    }
}
