using Backend.Models;

namespace Backend.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingByIdAsync(int id);
        Task<ApiResponse<Booking>> GetBookingsAsync(int roomId, int customerId, DateTime checkInDate, DateTime checkOutDate, string status);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task<bool> ConfirmBookingAsync(int id);
        Task<bool> CancelBookingAsync(int id);
    }
}
