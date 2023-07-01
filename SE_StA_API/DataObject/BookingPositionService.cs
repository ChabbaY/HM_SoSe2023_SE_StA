using System;
using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class BookingPositionService {
        [Key]
        public int BookingPositionServiceId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public DateTime DateTime { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public double Price { get; set; }

        public int ServiceId { get; internal set; }

        public int BookingId { get; internal set; }

    }
}