using System;
using System.Collections.Generic;
using System.Text;

namespace TripFlip.DataAccess.Entities
{
    public class ItemListEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int RouteId { get; set; }

        public DateTimeOffset DateCreated { get; set; }
    }
}
