using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces.TripInterfaces
{
    /// <summary>
    /// Interface that describes supported CRUD operations with Trip entity.
    /// </summary>
    public interface ITripService
    {
        /// <summary>
        /// Gets all Trips.
        /// </summary>
        /// <param name="paginationDto">Pagination parameters.</param>
        /// <param name="searchString">String to filter Trips.</param>
        /// <returns>Paged list of Trip dtos that represent the database entries.</returns>
        Task<PagedList<TripDto>>GetAllTripsAsync(
            string searchString,
            PaginationDto paginationDto);

        /// <summary>
        /// Gets Trip by id.
        /// </summary>
        /// <param name="id">Trip id.</param>
        /// <returns>Trip dto that represents the database entry with the given id.</returns>
        Task<TripDto> GetByIdAsync(int id);

        /// <summary>
        /// Creates new Trip.
        /// </summary>
        /// <param name="createTripDto">Data to create a Trip.</param>
        /// <returns>Trip dto that represents the new entry that was added to database.</returns>
        Task<TripDto> CreateAsync(CreateTripDto createTripDto);

        /// <summary>
        /// Updates existing Trip.
        /// </summary>
        /// <param name="tripDto">New Trip data with existing Trip id.</param>
        /// <returns>Trip dto that represents the updated database entry.</returns>
        Task<TripDto> UpdateAsync(TripDto tripDto);

        /// <summary>
        /// Deletes Trip.
        /// </summary>
        /// <param name="id">Trip id.</param>
        Task DeleteByIdAsync(int id);
    }
}
