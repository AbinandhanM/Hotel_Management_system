using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models
{
    public class Reservation
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        public uint UserId { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public int HotelId { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
