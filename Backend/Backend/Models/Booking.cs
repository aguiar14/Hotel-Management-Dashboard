namespace Backend.Models
{
    public class Booking
    {

        public int Id { get; set; }
        public string? Notes { get; set; }
        public Customer? Customer { get; set; }
        public Room? Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
