using System;
using System.Collections.Generic;
using TripFlip.ViewModels.ItemViewModels;

namespace TripFlip.ViewModels.ItemListViewModels
{
    public class ItemListWithItemsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<ItemWithoutListIdViewModel> Items { get; set; }
    }
}
