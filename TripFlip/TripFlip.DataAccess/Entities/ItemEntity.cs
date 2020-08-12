namespace TripFlip.Domain.Entities.Entities
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
    }
}
