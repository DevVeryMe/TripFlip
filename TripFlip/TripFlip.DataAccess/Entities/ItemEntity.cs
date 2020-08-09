
namespace TripFlip.DataAccess.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public string Quantity { get; set; }

        public int ItemListId { get; set; }

        public bool IsCompleted { get; set; }
    }
}
