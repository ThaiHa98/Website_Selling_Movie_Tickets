using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Domain.Entities.Enum;

namespace Website_Selling_Movie_Tickets.Infrastructure.Persistence
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TimeSlot> TimeSlots { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Theater> Theaters { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<ChairType> ChairTypes { get; set; }
        public virtual DbSet<ScreeningRoom> ScreeningRooms { get; set; }
        public virtual DbSet<SubtitleTable> SubtitleTables { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<PopcornandDrink> PopcornandDrinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slide>()
                .Property(x => x.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Ticket>()
                .Property(x => x.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Ticket>()
                .Property(x => x.ToatalPriceTicket)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Ticket>()
                .Property(x => x.PopcornandDrinks_Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Ticket>()
                .Property(x => x.ToatalPricePopcornandDrinks)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ChairType>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<User>()
                .Property(x => x.Roles)
                .HasConversion(
                 v => v.ToString(),
                 v => (Roles)Enum.Parse(typeof(Roles), v));

            modelBuilder.Entity<ScreeningRoom>()
                .Property(x => x.Status)
                .HasConversion(
                 v => v.ToString(),
                 v => (StatusScreenigRoom)Enum.Parse(typeof(StatusScreenigRoom), v));

            modelBuilder.Entity<Movie>()
                .Property(x => x.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Seat>()
                .Property(x => x.Status)
                .HasConversion<string>();

            modelBuilder.Entity<SubtitleTable>()
                .Ignore(e => e.TimeSlot_Id)
                .Property(e => e.TimeSlot_Id);

            modelBuilder.Entity<PopcornandDrink>()
                .Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<PopcornandDrink>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
