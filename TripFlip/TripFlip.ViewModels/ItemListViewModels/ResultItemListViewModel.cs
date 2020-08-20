using System;

namespace TripFlip.ViewModels.ItemListViewModels
{
    public class ResultItemListViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int RouteId { get; set; }
    }
}
