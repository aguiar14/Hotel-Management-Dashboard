using Backend.Data.Entities;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repositories
{
    public class RoomTypeSqlServerRepository : IRoomTypeRepository
    {

        private readonly DbContext _context;

        public RoomTypeSqlServerRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<RoomType> CreateRoomTypeAsync(RoomType roomType)
        {
            var entityRoomType = new RoomTypeEntity
            {
                Name = roomType.Name,
                Description = roomType.Description,
                CreatedAt = DateTime.UtcNow,
            };
            _context.RoomType.Add(entityRoomType);
            await _context.SaveChangesAsync();
            roomType.Id = entityRoomType.Id;

            return roomType;
        }

        public async Task<RoomType?> GetRoomTypeByIdAsync(int id)
        {
            var roomType = await _context.RoomType
                .Where(rt => rt.Id == id)
                .Select(rt => new RoomType
                {
                    Id = rt.Id,
                    Name = rt.Name,
                    Description = rt.Description,
                })
                .FirstOrDefaultAsync();
            return roomType;
        }

        public async Task<ApiResponse<RoomType>> GetRoomTypesAsync()
        {
            IQueryable<RoomTypeEntity> query = _context.RoomType;

            var count = await query.CountAsync();

            var items = await query
                .Select(rt => new RoomType
                {
                    Id = rt.Id,
                    Name = rt.Name,
                    Description = rt.Description,
                })
                .ToListAsync();

            return new ApiResponse<RoomType>
            {
                items = items,
                TotalCount = count
            };
        }

        public async Task<RoomType?> UpdateRoomTypeAsync(RoomType roomType)
        {
            var existingRoomType = await _context.RoomType.FirstOrDefaultAsync(rt => rt.Id == roomType.Id);
            if (existingRoomType == null)
            {
                return null;
            }
            existingRoomType.Name = roomType.Name;
            existingRoomType.Description = roomType.Description;
            existingRoomType.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return roomType;
        }

        public async Task<bool> DeleteRoomTypeAsync(int id)
        {
            var existingRoomType = await _context.RoomType.FirstOrDefaultAsync(rt => rt.Id == id);
            if (existingRoomType == null)
            {
                return false;
            }
            _context.RoomType.Remove(existingRoomType);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
