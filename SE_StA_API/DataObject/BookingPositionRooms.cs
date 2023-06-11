using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class BookingPositionRooms {
        [Key]
        public int BookingPositionRoomId { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string Date { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public double Price { get; set; }
    }
}