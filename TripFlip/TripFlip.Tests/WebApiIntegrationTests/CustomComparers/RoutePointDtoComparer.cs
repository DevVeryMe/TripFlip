using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.RoutePointDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class RoutePointDtoComparer : IComparer<RoutePointDto>
    {
        public int Compare(RoutePointDto x, RoutePointDto y)
        {
            if (x is null && y is null)
            {
                return 0;
            }

            if (x is null)
            {
                return -1;
            }

            if (y is null)
            {
                return 1;
            }

            if (x.Id.CompareTo(y.Id) != 0)
            {
                return x.Id.CompareTo(y.Id);
            }

            if (x.Longitude.CompareTo(y.Longitude) != 0)
            {
                return x.Longitude.CompareTo(y.Longitude);
            }

            if (x.Latitude.CompareTo(y.Latitude) != 0)
            {
                return x.Latitude.CompareTo(y.Latitude);
            }

            if (x.Order.CompareTo(y.Order) != 0)
            {
                return x.Order.CompareTo(y.Order);
            }

            return 0;
        }
    }
}
