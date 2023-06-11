using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Room {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string RoomNumber { get; set; }

    }
}