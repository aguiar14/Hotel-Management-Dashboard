using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities
{
    public class RoomEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int RoomTypeId { get; set; }

        public required RoomTypeEntity RoomType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
