
using Backend.Models;
namespace Backend.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<RoomType?> GetRoomTypeByIdAsync(int id);
        Task<ApiResponse<RoomType>> GetRoomTypesAsync();
        Task<RoomType> CreateRoomTypeAsync(RoomType roomType);
        Task<RoomType> UpdateRoomTypeAsync(RoomType roomType);
        Task<bool> DeleteRoomTypeAsync(int id);
    }
}
