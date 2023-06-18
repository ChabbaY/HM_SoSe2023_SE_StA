using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Room {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string RoomNumber { get; set; }

        public int HotelId { get; internal set; }
        
        public int RoomTypeId { get; internal set; }
        
        public List<BookingPositionRoom> bookingPositionRooms { get; internal set; }

    }
}