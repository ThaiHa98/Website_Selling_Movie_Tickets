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
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<Theater> Theaters { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<ChairType> ChairTypes { get; set; }
        public virtual DbSet<ScreeningRoom> ScreeningRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slide>()
                .Property(x => x.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Tickets>()
                .Property(x => x.ChairType_Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Tickets>()
                .Property(x => x.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Tickets>()
                .Property(x => x.ChairType_Price)
                .HasColumnType("decimal(18,2)");

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
        }
    }
}
