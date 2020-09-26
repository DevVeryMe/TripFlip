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
                if (x.TripAdmins.Count() < y.TripAdmins.Count())
                {
                    return -1;
                }

                if (x.TripAdmins.Count() > y.TripAdmins.Count())
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
                if (x.TripEditors.Count() < y.TripEditors.Count())
                {
                    return -1;
                }

                if (x.TripEditors.Count() > y.TripEditors.Count())
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
                if (x.TripGuests.Count() < y.TripGuests.Count())
                {
                    return -1;
                }

                if (x.TripGuests.Count() > y.TripGuests.Count())
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
