using Backend.Data.Entities;    
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options)
            : base(options) { }

        public DbSet<RoomEntity> Room { get; set; }
        public DbSet<RoomTypeEntity> RoomType { get; set; }
        public DbSet<CustomerEntity> Customer { get; set; }
        public DbSet<BookingEntity> Booking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomEntity>().ToTable("Rooms");


            modelBuilder.Entity<RoomEntity>().HasKey(r => r.Id);

            modelBuilder.Entity<RoomEntity>()
                .Property(r => r.Number)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<RoomEntity>()
                .Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<RoomEntity>()
                .Property(r => r.Capacity)
                .IsRequired();

            modelBuilder.Entity<RoomEntity>()
                .Property(r => r.PricePerNight)
                .IsRequired();

            modelBuilder.Entity<RoomEntity>()
                .Property(r => r.IsAvailable)
                .HasDefaultValue(true);


            //------------------Room Type --------------

            modelBuilder.Entity<RoomTypeEntity>()
                .ToTable("RoomTypes");



            modelBuilder.Entity<RoomTypeEntity>().HasKey(rt => rt.Id);

            modelBuilder.Entity<RoomTypeEntity>()
                .Property(rt => rt.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<RoomTypeEntity>()
                .Property(rt => rt.Description)
                .IsRequired()
                .HasMaxLength(500);


            modelBuilder.Entity<RoomEntity>()
                .HasOne(r => r.RoomType)
                .WithMany()
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();


            //------------------Customer  --------------

            modelBuilder.Entity<CustomerEntity>().ToTable("Customers");

            modelBuilder.Entity<CustomerEntity>().HasKey(c => c.Id);

            modelBuilder.Entity<CustomerEntity>().Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<CustomerEntity>().Property(c => c.LastName)
                .IsRequired();

            modelBuilder.Entity<CustomerEntity>().Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<CustomerEntity>().Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<CustomerEntity>().Property(c => c.Country);



          //--------------------  Booking -----------------


            modelBuilder.Entity<BookingEntity>().ToTable("Bookings");

            modelBuilder.Entity<BookingEntity>().HasKey(b => b.Id);

            modelBuilder.Entity<BookingEntity>().Property(b=>b.Notes)
                .HasMaxLength(500);

            modelBuilder.Entity<BookingEntity>().HasOne(b => b.Customer)
                    .WithMany()
                    .HasForeignKey(b => b.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

            modelBuilder.Entity<BookingEntity>().HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey(b => b.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

            modelBuilder.Entity<BookingEntity>().Property(b => b.CheckInDate)
                .IsRequired();

            modelBuilder.Entity<BookingEntity>().Property(b => b.CheckOutDate)
                .IsRequired();

            modelBuilder.Entity<BookingEntity>().Property(b => b.Status);

            modelBuilder.Entity<BookingEntity>().Property(b => b.TotalPrice);



            // --------------------------  Seed data --------------------------------------

            modelBuilder.Entity<RoomTypeEntity>().HasData(
                new RoomTypeEntity
                {
                    Id = 1,
                    Name = "Single",
                    Description = "A room for one person.",
                    CreatedAt = new DateTime(2025, 8, 4),
                    UpdatedAt = new DateTime(2025, 8, 4)
                },
                new RoomTypeEntity
                {
                    Id = 2,
                    Name = "Double",
                    Description = "A room for two persons.",
                    CreatedAt = new DateTime(2025, 8, 4),
                    UpdatedAt = new DateTime(2025, 8, 4)
                },
                new RoomTypeEntity
                {
                    Id = 3,
                    Name = "Suite",
                    Description = "A luxurious suite with additional amenities.",
                    CreatedAt = new DateTime(2025, 8, 4),
                    UpdatedAt = new DateTime(2025, 8, 4)
                }
            );

            // In the .HasData() calls for RoomEntity, set RoomType to null to satisfy the required property for seeding.
            // EF Core will not use navigation properties during seeding, so setting RoomType = null is correct.

            modelBuilder.Entity<RoomEntity>().HasData(
                new RoomEntity
                {
                    Id = 1,
                    Number = "101",
                    Description = "A cozy single room with a comfortable bed.",
                    Capacity = 1,
                    PricePerNight = 100.00m,
                    IsAvailable = true,
                    RoomTypeId = 1,
                    RoomType = null!,
                    CreatedAt = new DateTime(2025, 8, 4),
                    UpdatedAt = new DateTime(2025, 8, 4)
                },
                new RoomEntity
                {
                    Id = 2,
                    Number = "102",
                    Description = "A spacious double room with two beds.",
                    Capacity = 2,
                    PricePerNight = 150.00m,
                    IsAvailable = true,
                    RoomTypeId = 2,
                    RoomType = null!,
                    CreatedAt = new DateTime(2025, 8, 4),
                    UpdatedAt = new DateTime(2025, 8, 4)
                },
                new RoomEntity
                {
                    Id = 3,
                    Number = "103",
                    Description = "A luxurious suite with a king-size bed and a view.",
                    Capacity = 2,
                    PricePerNight = 300.00m,
                    IsAvailable = true,
                    RoomTypeId = 3,
                    RoomType = null!,
                    CreatedAt = new DateTime(2025, 8, 4),
                    UpdatedAt = new DateTime(2025, 8, 4)
                }
            );
        }


    }


}