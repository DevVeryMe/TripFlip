using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TripDtos;

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
        Task<TripDto> GetAsync(int id);

        /// <summary>
        /// Creates new trip.
        /// </summary>
        /// <param name="createTripDto">new trip</param>
        /// <returns>created trip</returns>
        Task<TripDto> CreateAsync(CreateTripDto createTripDto);
        Task<TripDto> UpdateAsync(TripDto tripDto);
    }
}
