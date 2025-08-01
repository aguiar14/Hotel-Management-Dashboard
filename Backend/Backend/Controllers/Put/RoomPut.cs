using Backend.Controllers.Post;
using Backend.Models;
namespace Backend.Controllers.Put
{
    public class RoomPut
    {

        public string? Number { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int Capacity { get; set; } = 1;
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; } = true;
        public required RoomType RoomType { get; set; }
        // Additional properties can be added here if needed

        public Room ToRoomModel(int id)
        {
            return new Room
            {
                Id = id,
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
