namespace TripFlip.Domain.Entities.Entities
{
    public class TripFileEntity
    {
        public int Id { get; set; }
        public string FileUrl { get; set; }
        public int TripId { get; set; }
        public string Title { get; set; }

        public TripEntity Trip { get; set; }
    }
}
