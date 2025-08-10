using Backend.Data.Entities;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Backend.Data.Repositories
{
    public class BookingSqlRepository: IBookingRepositoty
    {
        private readonly DbContext _context;

        public BookingSqlRepository(DbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {

            var exisitingBookedRoom = await _context.Booking
                .Where(b => b.RoomId == booking.Room.Id &&
                            b.CheckInDate < booking.CheckOutDate &&
                            b.CheckOutDate > booking.CheckInDate &&
                            b.Status != "Cancelled")
                .FirstOrDefaultAsync();

            var roomTobebooked = await _context.Room.FirstOrDefaultAsync(r => r.Id == booking.Room.Id);

            if (exisitingBookedRoom == null)
            {
                throw new InvalidOperationException("The room is already booked for the selected dates.");
            }
    
            var entityBooking = new BookingEntity
            {
                Notes = booking.Notes,
                CustomerId = booking.Customer.Id,
                RoomId = booking.Room.Id,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                Status = booking.Status,
                TotalPrice = booking.TotalPrice,
                CreatedAt = DateTime.UtcNow,
            };

            roomTobebooked.IsAvailable = false;

            _context.Booking.Add(entityBooking);
            _context.Room.Update(roomTobebooked);
           
            await _context.SaveChangesAsync();
            booking.Id = entityBooking.Id;

            return booking;
           

            

        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Booking
                .Where(b => b.Id == id)
                .Select(b => new Booking
                {
                    Id = b.Id,
                    Notes = b.Notes,

                    Customer = new Customer
                    {
                        Id = b.Customer.Id,
                        FirstName = b.Customer.FirstName,
                        LastName = b.Customer.LastName,
                        Email = b.Customer.Email,
                        PhoneNumber = b.Customer.PhoneNumber,
                        Country = b.Customer.Country
                    },

                    Room = new Room
                    {
                        Id = b.Room.Id,
                        Description = b.Room.Description,
                        Number = b.Room.Number,
                        Capacity = b.Room.Capacity,
                        PricePerNight = b.Room.PricePerNight,
                        IsAvailable = b.Room.IsAvailable,
                        RoomType = new RoomType
                        {
                            Id = b.Room.RoomType.Id,
                            Name = b.Room.RoomType.Name,
                            Description = b.Room.RoomType.Description
                        }

                    },
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    Status = b.Status,
                    TotalPrice = b.TotalPrice
                })
                .FirstOrDefaultAsync();

            return booking ?? null;
        }

        public async Task<ApiResponse<Booking>> GetBookingsAsync(int roomId, int customerId, DateTime? checkInDate, DateTime? checkOutDate, string status)
        {
            IQueryable<BookingEntity> query = _context.Booking;

            
            if (roomId > 0)
            {
                query = query.Where(b => b.Room.Id == roomId);
            }

            if(customerId > 0)
            {
                query = query.Where(b => b.Customer.Id == customerId);
            }

            if (checkInDate != default(DateTime))
            {
                query = query.Where(b => b.CheckInDate >= checkInDate.Value);
            }

            if (checkOutDate!= default(DateTime))
            {
                query = query.Where(b => b.CheckOutDate == checkOutDate.Value);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(b => b.Status == status);
            }

            var items = await query
                .Select(b => new Booking
                {
                    Id = b.Id,
                    Notes = b.Notes,

                    Customer = new Customer
                    {
                        Id = b.Customer.Id,
                        FirstName = b.Customer.FirstName,
                        LastName = b.Customer.LastName,
                        Email = b.Customer.Email,
                        PhoneNumber = b.Customer.PhoneNumber,
                        Country = b.Customer.Country
                    },

                    Room = new Room
                    {
                        Id = b.Room.Id,
                        Description = b.Room.Description,
                        Number = b.Room.Number,
                        Capacity = b.Room.Capacity,
                        PricePerNight = b.Room.PricePerNight,
                        IsAvailable = b.Room.IsAvailable,
                        RoomType = new RoomType
                        {
                            Id = b.Room.RoomType.Id,
                            Name = b.Room.RoomType.Name,
                            Description = b.Room.RoomType.Description
                        }

                    },
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    Status = b.Status,
                    TotalPrice = b.TotalPrice
                })
                .ToListAsync();

            var count = query.Count();

            return new ApiResponse<Booking>
            {
                items = items,
                TotalCount = count
            };
        }

        public async Task<Booking?> UpdateBookingAsync(Booking booking)
        {
            var exisitingBooking = await _context.Booking.FirstOrDefaultAsync(b => b.Id == booking.Id);

            if(exisitingBooking == null)
            {
                return null;
            }

            exisitingBooking.Notes = booking.Notes;
            exisitingBooking.CustomerId = booking.Customer.Id;
            exisitingBooking.RoomId = booking.Room.Id;
            exisitingBooking.CheckInDate = booking.CheckInDate;
            exisitingBooking.CheckOutDate = booking.CheckOutDate;
            exisitingBooking.Status = booking.Status;
            exisitingBooking.TotalPrice = booking.TotalPrice;
            exisitingBooking.UpdatedAt = DateTime.UtcNow;
        

            _context.Booking.Update(exisitingBooking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<bool> ConfirmBookingAsync(int id)
        {
            var exisitingBokking = await _context.Booking.FirstOrDefaultAsync(b => b.Id == id);
            if (exisitingBokking == null)
            {
                return false;
            }

            exisitingBokking.Status = "Confirmed";
            exisitingBokking.UpdatedAt = DateTime.UtcNow;
            _context.Booking.Update(exisitingBokking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelBookingAsync(int id)
        {
            var exisitingBokking = await _context.Booking.FirstOrDefaultAsync(b => b.Id == id);
            if (exisitingBokking == null)
            {
                return false;
            }

            exisitingBokking.Status = "Cancelled";
            exisitingBokking.UpdatedAt = DateTime.UtcNow;
            _context.Booking.Update(exisitingBokking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
