using Backend.Models;

namespace Backend.Controllers.Put
{
    public class BookingPut
    {
        public string? Notes { get; set; } = string.Empty;
        public Customer? Customer { get; set; } = null;
        public Room? Room { get; set; } = null;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; } = 0;

        public Booking ToBookingModel(int id)
        {
            return new Booking
            {
               Id = id,
                Notes = this.Notes,
                Customer = this.Customer,
                Room = this.Room,
                CheckInDate = this.CheckInDate,
                CheckOutDate = this.CheckOutDate,
                TotalPrice = this.TotalPrice
            };
        }
    }
}
