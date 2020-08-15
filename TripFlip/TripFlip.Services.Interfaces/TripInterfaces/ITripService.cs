using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces.TripInterfaces
{
    public interface ITripService
    {
        Task<IEnumerable<TripDto>>GetAllTripsAsync();

        TripDto GetTrip(int id);

        void CreateTrip(TripDto tripDto);

        void UpdateTrip(TripDto tripDto);

        void DeleteTrip(TripDto tripDto);
    }
}
