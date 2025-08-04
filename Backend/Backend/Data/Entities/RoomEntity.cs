using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities
{
    public class RoomEntity
    {

        [Key] public int Id { get; set; }
        [Required] public string? Number { get; set; }
        [Required] public string? Description { get; set; }
        [Required] public int Capacity { get; set; }
        [Required] public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int RoomTypeId { get; set; }

        public  RoomTypeEntity? RoomType { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
