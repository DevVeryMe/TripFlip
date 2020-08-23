using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TripDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces.TripInterfaces
{
    public interface ITripService
    {
        /// <summary>
        /// Returns page of Trips.
        /// </summary>
        /// <param name="paginationDto">Object that represents the pagination parameters.</param>
        /// <param name="titleSearchString">String to filter trips by Title.</param>
        /// <param name="descriptionSearchString">String to filter trips by Description.</param>
        /// <returns><see cref="PagedList{TripDto}"/> object that represents the paged collection of database entries.</returns>
        Task<PagedList<TripDto>>GetAllTripsAsync(PaginationDto paginationDto,
            string titleSearchString,
            string descriptionSearchString);

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

        /// <summary>
        /// Updates existing trip.
        /// </summary>
        /// <param name="tripDto">new trip data</param>
        /// <returns>updated trip</returns>
        Task<TripDto> UpdateAsync(TripDto tripDto);

        /// <summary>
        /// Deletes existing trip.
        /// </summary>
        /// <param name="id">trip id</param>
        Task DeleteAsync(int id);
    }
}
