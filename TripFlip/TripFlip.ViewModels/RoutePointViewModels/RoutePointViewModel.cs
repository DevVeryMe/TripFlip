using System;

namespace TripFlip.ViewModels.RoutePointViewModels
{
    public class RoutePointViewModel
    {
        public int Id { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public int Order { get; set; }
    }
}
