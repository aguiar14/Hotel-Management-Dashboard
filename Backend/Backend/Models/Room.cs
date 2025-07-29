namespace Backend.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int RoomTypeId { get; set; }


    }
}
