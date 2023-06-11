using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Booking {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Number { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string Date { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public double TotalPrice { get; set; }

    }
}