using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Models
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
