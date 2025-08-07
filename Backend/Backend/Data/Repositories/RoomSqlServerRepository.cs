using Backend.Data.Entities;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repositories
{
    public class RoomSqlServerRepository: IRoomRepository
    {
        private readonly DbContext _context;

        public RoomSqlServerRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {

            var entityRoom = new RoomEntity
            {
                Number = room.Number,
                Description = room.Description,
                Capacity = room.Capacity,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable,
                RoomTypeId = room.RoomType.Id,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Room.Add(entityRoom);
            await _context.SaveChangesAsync();
            room.Id = entityRoom.Id;

            return room;

        }
        public async Task<Room?> GetRoomByIdAsync(int id)
        {

            var room = await _context.Room
                .Where(r => r.Id == id)
                .Select(r => new Room {
                    Id = r.Id,
                    Description = r.Description,
                    Number = r.Number,
                    Capacity = r.Capacity,
                    PricePerNight = r.PricePerNight,
                    IsAvailable = r.IsAvailable,
                    RoomType = new RoomType
                    {
                        Id = r.RoomType.Id,
                        Name = r.RoomType.Name,
                        Description = r.RoomType.Description
                    },
                })
                .FirstOrDefaultAsync();
            return room;

        }
        public async Task<ApiResponse<Room>> GetRoomsAsync(int roomType, int isAvailable, int capacity)
        {
            IQueryable<RoomEntity> query = _context.Room;

            var count = await query.CountAsync();

            if(roomType != -1)
            {
                  query = query.Where(r => r.RoomTypeId == roomType);
            }

            if(isAvailable != -1)
            {
                query = query.Where(r => r.IsAvailable == (isAvailable == 1));
            }

            if(capacity != -1 && capacity <= 5)
            {
                query = query.Where(r => r.Capacity == capacity);
            }else if(capacity > 5) { query = query.Where(r => r.Capacity > 5); }

            var items = await query
                    .Select(r => new Room
                    {
                        Id = r.Id,
                        Number = r.Number,
                        Description = r.Description,
                        Capacity = r.Capacity,
                        PricePerNight = r.PricePerNight,
                        IsAvailable = r.IsAvailable,
                        RoomType = new RoomType
                        {
                            Id = r.RoomType.Id,
                            Name = r.RoomType.Name,
                            Description = r.RoomType.Description
                        },

                    })
                    .ToListAsync();

            return new ApiResponse<Room> { items = items, TotalCount = count };
        }
      
        public async Task<Room?> UpdateRoomAsync(Room room)
        {
            var existingRoom = await _context.Room.FirstOrDefaultAsync(r => r.Id == room.Id);

            if(existingRoom == null)
            {
                return null;
            }

            existingRoom.Number = room.Number ?? throw  new InvalidOperationException();
            existingRoom.Description = room.Description;
            existingRoom.Capacity = room.Capacity;
            existingRoom.PricePerNight = room.PricePerNight;
            existingRoom.IsAvailable = room.IsAvailable;
            existingRoom.RoomTypeId = room.RoomType.Id;
            existingRoom.UpdatedAt = DateTime.UtcNow;
            _context.Room.Update(existingRoom);
            await _context.SaveChangesAsync();

            return room;
        }
        public async Task<bool> DeleteRoomAsync(int id)
        {
            var existingRoom = await _context.Room.FirstOrDefaultAsync(r => r.Id == id);

            if (existingRoom == null)
            {
                return false;
            }

            _context.Room.Remove(existingRoom);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
