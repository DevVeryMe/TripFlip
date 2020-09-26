using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.Enums;
using TripFlip.Services.Dto.UserDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class UserDtoComparer : IComparer<UserDto>
    {
        public int Compare(UserDto x, UserDto y)
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

            if (string.Compare(x.Email, y.Email, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Email, y.Email, StringComparison.Ordinal);
            }

            if (string.Compare(x.FirstName, y.FirstName, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.FirstName, y.FirstName, StringComparison.Ordinal);
            }

            if (string.Compare(x.LastName, y.LastName, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.LastName, y.LastName, StringComparison.Ordinal);
            }

            if (string.Compare(x.AboutMe, y.AboutMe, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.AboutMe, y.AboutMe, StringComparison.Ordinal);
            }

            if (x.BirthDate == null && y.BirthDate != null)
            {
                return -1;
            }

            if (x.BirthDate != null && y.BirthDate == null)
            {
                return 1;
            }

            if (x.BirthDate != null && y.BirthDate != null &&
                DateTimeOffset.Compare((DateTimeOffset)x.BirthDate, (DateTimeOffset)y.BirthDate) != 0)
            {
                return DateTimeOffset.Compare((DateTimeOffset)x.BirthDate, (DateTimeOffset)y.BirthDate);
            }

            if (x.Gender == null && y.Gender != null)
            {
                return -1;
            }

            if (x.Gender != null && y.Gender == null)
            {
                return 1;
            }

            if (x.Gender != null && y.Gender != null &&
                ((UserGender)x.Gender).CompareTo((UserGender)y.Gender) != 0)
            {
                return ((UserGender)x.Gender).CompareTo((UserGender)y.Gender);
            }

            return 0;
        }
    }
}
