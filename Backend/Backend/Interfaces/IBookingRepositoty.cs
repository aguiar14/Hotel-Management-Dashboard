using Backend.Models;

namespace Backend.Interfaces
{
    public interface IBookingRepositoty
    {
        Task<Booking> GetBookingByIdAsync(int id);
        Task<ApiResponse<Booking>> GetBookingsAsync(int roomId, int userId, DateTime? startDate, DateTime? endDate, string status);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task<bool> ConfirmBookingAsync(int id);
        Task<bool> CancelBookingAsync(int id);
    }
}
