using TripFlip.Services.DTO.Enums;

namespace TripFlip.Services.DTO.ItemDtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public string Quantity { get; set; }

        public bool IsCompleted { get; set; }

        public int ItemListId { get; set; }
    }
}
