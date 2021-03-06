﻿using System.Collections.Generic;

namespace TripFlip.Domain.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public string Quantity { get; set; }

        public bool IsCompleted { get; set; }

        public int ItemListId { get; set; }
        public ItemListEntity ItemList { get; set; }

        public ICollection<ItemAssigneeEntity> ItemAssignees { get; set; }
    }
}
