using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities
{
    public class BookingEntity
    {
        [Key]public int Id { get; set; }
        public string? Notes { get; set; }
        public CustomerEntity? Customer { get; set; }
        public int CustomerId { get; set; }
        public RoomEntity? Room { get; set; }

        public int RoomId { get; set; }
        [Required] public DateTime CheckInDate { get; set; }
        [Required] public DateTime CheckOutDate { get; set; }
        [Required] public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
