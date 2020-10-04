using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.TripRoleDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class TripRoleDtoComparer : IComparer<TripRoleDto>
    {
        public int Compare(TripRoleDto x, TripRoleDto y)
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

            if (string.Compare(x.Name, y.Name, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            }

            return 0;
        }
    }
}
