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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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


            //--------------------------------

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



            base.OnModelCreating(modelBuilder);
        }


    }


}