using System;
using System.Collections.Generic;
using TripFlip.ViewModels.ItemViewModels;

namespace TripFlip.ViewModels.ItemListViewModels
{
    public class ItemListWithIncludesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<ItemViewModel> Items { get; set; }
    }
}
