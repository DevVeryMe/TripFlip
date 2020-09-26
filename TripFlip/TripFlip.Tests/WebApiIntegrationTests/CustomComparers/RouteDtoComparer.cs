using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.RouteDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class RouteDtoComparer : IComparer<RouteDto>
    {
        public int Compare(RouteDto x, RouteDto y)
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

            if (x.TripId.CompareTo(y.TripId) != 0)
            {
                return x.TripId.CompareTo(y.TripId);
            }

            if (string.Compare(x.Title, y.Title, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Title, y.Title, StringComparison.Ordinal);
            }

            if (DateTimeOffset.Compare(x.DateCreated, y.DateCreated) != 0)
            {
                return DateTimeOffset.Compare(x.DateCreated, y.DateCreated);
            }

            return 0;
        }
    }
}
