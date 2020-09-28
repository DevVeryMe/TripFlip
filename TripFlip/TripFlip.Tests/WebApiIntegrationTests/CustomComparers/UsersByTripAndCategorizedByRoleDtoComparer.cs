using System.Collections.Generic;
using System.Linq;
using TripFlip.Services.Dto.UserDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class UsersByTripAndCategorizedByRoleDtoComparer : 
        IComparer<UsersByTripAndCategorizedByRoleDto>
    {
        public int Compare(UsersByTripAndCategorizedByRoleDto x, 
            UsersByTripAndCategorizedByRoleDto y)
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

            if (x.TripAdmins == null && y.TripAdmins != null)
            {
                return -1;
            }

            if (x.TripAdmins != null && y.TripAdmins == null)
            {
                return 1;
            }

            var userDtoComparer = new UserDtoComparer();

            if (x.TripAdmins != null && y.TripAdmins != null)
            {
                var xTripAdminsCount = x.TripAdmins.Count();
                var yTripAdminsCount = y.TripAdmins.Count();

                if (xTripAdminsCount < yTripAdminsCount)
                {
                    return -1;
                }

                if (xTripAdminsCount > yTripAdminsCount)
                {
                    return 1;
                }

                foreach (var item in x.TripAdmins)
                {
                    var result = userDtoComparer.Compare(
                        item, y.TripAdmins.FirstOrDefault(y => y.Id == item.Id));

                    if (result != 0)
                    {
                        return result;
                    }
                }
            }

            if (x.TripEditors != null && y.TripEditors != null)
            {
                var xTripEditorsCount = x.TripEditors.Count();
                var yTripEditorsCount = y.TripEditors.Count();

                if (xTripEditorsCount < yTripEditorsCount)
                {
                    return -1;
                }

                if (xTripEditorsCount > yTripEditorsCount)
                {
                    return 1;
                }

                foreach (var item in x.TripEditors)
                {
                    var result = userDtoComparer.Compare(
                        item, y.TripEditors.FirstOrDefault(y => y.Id == item.Id));

                    if (result != 0)
                    {
                        return result;
                    }
                }
            }

            if (x.TripGuests != null && y.TripGuests != null)
            {
                var xTripGuestsCount = x.TripGuests.Count();
                var yTripGuestsCount = y.TripGuests.Count();

                if (xTripGuestsCount < yTripGuestsCount)
                {
                    return -1;
                }

                if (xTripGuestsCount > yTripGuestsCount)
                {
                    return 1;
                }

                foreach (var item in x.TripGuests)
                {
                    var result = userDtoComparer.Compare(
                        item, y.TripGuests.FirstOrDefault(y => y.Id == item.Id));

                    if (result != 0)
                    {
                        return result;
                    }
                }
            }

            return 0;
        }
    }
}
