using Backend.Interfaces;
using Backend.Models;

namespace Backend.Data.Repositories
{
    public class RoomSqlServerRepository
    {
        private readonly DbContext _context;

        public RoomSqlServerRepository(DbContext context)
        {
            _context = context;
        }
        //public Task<Room> GetRoomByIdAsync(int id)
        //{
        //    return 

        //}
        //Task<ApiResponse<Room>> GetRoomsAsync();
        //Task<Room> CreateRoomAsync(Room room);
        //Task<Room> UpdateRoomAsync(Room room);
        //Task<bool> DeleteRoomAsync(int id);
    }
}
