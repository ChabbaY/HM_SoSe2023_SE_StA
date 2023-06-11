using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class BookingPositionRooms {
        [Key]
        public int BookingPositionRoomId { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public DateTime Date { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public double Price { get; set; }

        public int RoomId { get; internal set; }

        public int BookingId { get; internal set; }
    }
}