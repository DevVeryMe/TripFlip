using System;
using TripFlip.Services.Dto.ItemDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class ItemDtoComparer
    {
        public int Compare(ItemDto x, ItemDto y)
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

            if (string.Compare(x.Comment, y.Comment, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Comment, y.Comment, StringComparison.Ordinal);
            }

            if (string.Compare(x.Quantity, y.Quantity, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Quantity, y.Quantity, StringComparison.Ordinal);
            }

            if (x.ItemListId.CompareTo(y.ItemListId) != 0)
            {
                return x.ItemListId.CompareTo(y.ItemListId);
            }

            if (x.IsCompleted.CompareTo(y.IsCompleted) != 0)
            {
                return x.IsCompleted.CompareTo(y.IsCompleted);
            }

            return 0;
        }
    }
}
