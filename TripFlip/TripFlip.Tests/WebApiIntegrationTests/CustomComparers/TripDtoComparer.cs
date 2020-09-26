using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.TripDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class TripDtoComparer : IComparer<TripDto>
    {
        public int Compare(TripDto x, TripDto y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            if (x.Id.CompareTo(y.Id) != 0)
            {
                return x.Id.CompareTo(y.Id);
            }

            if (string.Compare(x.Title, y.Title, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Title, y.Title, StringComparison.Ordinal);
            }

            if(string.Compare(x.Description, y.Description, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Description, y.Description, StringComparison.Ordinal);
            }

            if (x.StartsAt == null && y.StartsAt != null)
            {
                return -1;
            }

            if (x.StartsAt != null && y.StartsAt == null)
            {
                return 1;
            }

            if (x.StartsAt != null && y.StartsAt != null &&
                DateTimeOffset.Compare((DateTimeOffset) x.StartsAt, (DateTimeOffset) y.StartsAt) != 0)
            {
                return DateTimeOffset.Compare((DateTimeOffset) x.StartsAt, (DateTimeOffset) y.StartsAt);
            }

            if (x.EndsAt == null && y.EndsAt != null)
            {
                return -1;
            }

            if (x.EndsAt != null && y.EndsAt == null)
            {
                return 1;
            }

            if (x.EndsAt != null && y.EndsAt != null &&
                DateTimeOffset.Compare((DateTimeOffset)x.EndsAt, (DateTimeOffset)y.EndsAt) != 0)
            {
                return DateTimeOffset.Compare((DateTimeOffset)x.EndsAt, (DateTimeOffset)y.EndsAt);
            }

            return 0;
        }
    }
}
