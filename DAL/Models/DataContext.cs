using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public partial class DataContext: DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<User>();
            modelBuilder.Entity<Room>();
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(a => a.User)
                .WithMany(a => a.Reservations)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Reservation");

                entity.HasOne(a => a.Room)
                .WithMany(a => a.Reservations)
                .HasForeignKey(a => a.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Room_Reservation");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
