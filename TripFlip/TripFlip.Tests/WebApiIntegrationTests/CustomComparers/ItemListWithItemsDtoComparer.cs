using System;
using System.Collections.Generic;
using System.Linq;
using TripFlip.Services.Dto.ItemListDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class ItemListWithItemsDtoComparer : IComparer<ItemListWithItemsDto>
    {
        public int Compare(ItemListWithItemsDto x, ItemListWithItemsDto y)
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

            #region Comparing nested objects
            if (x.Items is null && !(y.Items is null))
            {
                return -1;
            }

            if (!(x.Items is null) && y.Items is null)
            {
                return 1;
            }

            if (!(x.Items is null) && !(y.Items is null))
            {
                var xItemsArray = x.Items.ToArray();
                var yItemsArray = y.Items.ToArray();

                if (xItemsArray.Length < yItemsArray.Length)
                {
                    return -1;
                }

                if (xItemsArray.Length > yItemsArray.Length)
                {
                    return 1;
                }

                var comparer = new ItemWithAssigneesDtoComparer();

                for (int i = 0; i < xItemsArray.Length; i++)
                {
                    int result = comparer.Compare(xItemsArray[i], yItemsArray[i]);

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
