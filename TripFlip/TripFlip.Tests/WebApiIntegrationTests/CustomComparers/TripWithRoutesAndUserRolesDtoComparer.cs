using System;
using System.Collections.Generic;
using System.Linq;
using TripFlip.Services.Dto.TripDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class TripWithRoutesAndUserRolesDtoComparer
        : IComparer<TripWithRoutesAndUserRolesDto>
    {
        public int Compare(
            TripWithRoutesAndUserRolesDto x, TripWithRoutesAndUserRolesDto y)
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

            if (string.Compare(x.Title, y.Title, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Title, y.Title, StringComparison.Ordinal);
            }

            if (string.Compare(x.Description, y.Description, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Description, y.Description, StringComparison.Ordinal);
            }

            if (x.StartsAt is null && !(y.StartsAt is null))
            {
                return -1;
            }

            if (!(x.StartsAt is null) && y.StartsAt is null)
            {
                return 1;
            }

            if (!(x.StartsAt is null) && !(y.StartsAt is null) &&
                DateTimeOffset.Compare((DateTimeOffset)x.StartsAt, (DateTimeOffset)y.StartsAt) != 0)
            {
                return DateTimeOffset.Compare((DateTimeOffset)x.StartsAt, (DateTimeOffset)y.StartsAt);
            }

            if (x.EndsAt is null && !(y.EndsAt is null))
            {
                return -1;
            }

            if (!(x.EndsAt is null) && y.EndsAt is null)
            {
                return 1;
            }

            if (!(x.EndsAt is null) && !(y.EndsAt is null) &&
                DateTimeOffset.Compare((DateTimeOffset)x.EndsAt, (DateTimeOffset)y.EndsAt) != 0)
            {
                return DateTimeOffset.Compare((DateTimeOffset)x.EndsAt, (DateTimeOffset)y.EndsAt);
            }

            #region Comparing nested objects
            if (x.TripRoles is null && !(y.TripRoles is null))
            {
                return -1;
            }

            if (!(x.TripRoles is null) && y.TripRoles is null)
            {
                return 1;
            }

            if (!(x.TripRoles is null) && !(y.TripRoles is null))
            {
                var xTripRolesArray = x.TripRoles.ToArray();
                var yTripRolesArray = y.TripRoles.ToArray();

                if (xTripRolesArray.Length < yTripRolesArray.Length)
                {
                    return -1;
                }

                if (xTripRolesArray.Length > yTripRolesArray.Length)
                {
                    return 1;
                }

                var tripRoleDtoComparer = new TripRoleDtoComparer();

                for (int i = 0; i < xTripRolesArray.Length; i++)
                {
                    int result = tripRoleDtoComparer.Compare(
                        xTripRolesArray[i], yTripRolesArray[i]);

                    if (result != 0)
                    {
                        return result;
                    }
                }
            }

            if (x.Routes is null && !(y.Routes is null))
            {
                return -1;
            }

            if (!(x.Routes is null) && y.Routes is null)
            {
                return 1;
            }

            if (!(x.Routes is null) && !(y.Routes is null))
            {
                var xRoutesArray = x.Routes.ToArray();
                var yRoutesArray = y.Routes.ToArray();

                if (xRoutesArray.Length < yRoutesArray.Length)
                {
                    return -1;
                }

                if (xRoutesArray.Length > yRoutesArray.Length)
                {
                    return 1;
                }

                var routeWithNestedObjectsComparer = new RouteWithPointsItemAndTaskListsDtoComparer();

                for (int i = 0; i < xRoutesArray.Length; i++)
                {
                    int result = routeWithNestedObjectsComparer.Compare(
                        xRoutesArray[i], yRoutesArray[i]);

                    if (result != 0)
                    {
                        return result;
                    }
                }
            }
            #endregion

            return 0;
        }
    }
}
