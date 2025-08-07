using Backend.Models;

namespace Backend.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomByIdAsync(int id);
        Task<ApiResponse<Room>> GetRoomsAsync(int roomType, int isAvailable, int capacity);
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(int id);
    }
}
