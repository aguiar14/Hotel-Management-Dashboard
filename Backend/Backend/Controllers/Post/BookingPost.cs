using Backend.Models;

namespace Backend.Controllers.Post
{
    public class BookingPost
    {
      
        public string? Notes { get; set; } = string.Empty;
        public Customer? Customer { get; set; } = null;
        public Room? Room { get; set; } = null;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string? Status { get; set; }
        public decimal TotalPrice { get; set; } = 0;

        public Booking ToBookingModel()
        {
            return new Booking
            {
              
                Notes = this.Notes,
                Customer = this.Customer,
                Room = this.Room,
                CheckInDate = this.CheckInDate,
                CheckOutDate = this.CheckOutDate,
                Status = this.Status ?? "Pending", // Default status is "Pending"
                TotalPrice = this.TotalPrice
            };
        }
    }
}
