namespace TripFlip.Services.DTO.ItemDtos
{
    public class CreateItemDto
    {
        public string Title { get; set; }

        public string Comment { get; set; }

        public string Quantity { get; set; }

        public int ItemListId { get; set; }
    }
}
