using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.Models
{
    public class Room
    {
        [Key]
        public int R_id { get; set; }
        public int H_id { get; set; }
        [ForeignKey("H_id")]
        public Hotel? Hotels { get; set; }
        public int RoomNumber { get; set; }
        public double Price { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
