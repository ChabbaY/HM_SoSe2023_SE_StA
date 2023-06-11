using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Hotel {
        [Key]
        public int HotelId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1)]
        public int Sterne { get; set; }
    }
}