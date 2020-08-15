using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces.TripInterfaces
{
    public interface ITripService
    {
        Task<IEnumerable<TripDto>>GetAllTripsAsync();

        Task<TripDto> GetAsync(int id);

        void CreateTrip(TripDto tripDto);

        void UpdateTrip(TripDto tripDto);

        void DeleteTrip(TripDto tripDto);
    }
}
