using Backend.Models;
namespace Backend.Controllers.Post
{
    public class RoomPost
    {
        public string? Number { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int Capacity { get; set; } = 1;
        public decimal PricePerNight { get; set; } = 0.00m;
        public bool IsAvailable { get; set; } = true;
        public required RoomType RoomType { get; set; } // Default value, can be changed as needed
        // Additional properties can be added here if needed

        public Room ToRoomModel()
        {
            return new Room
            {
                Number = this.Number,
                Description = this.Description,
                Capacity = this.Capacity,
                PricePerNight = this.PricePerNight,
                IsAvailable = this.IsAvailable,
                RoomType = this.RoomType
            };

        }
    }
}
