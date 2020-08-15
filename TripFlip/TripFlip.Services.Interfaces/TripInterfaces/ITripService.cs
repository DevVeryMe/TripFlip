using System.Collections.Generic;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces.TripInterfaces
{
    public interface ITripService
    {
        IEnumerable<TripDto>GetAllTrips();

        TripDto GetTrip(int id);

        void CreateTrip(TripDto tripDto);

        void UpdateTrip(TripDto tripDto);
    }
}
