using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Flight {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string FlightNumber { get;  set; }
        
        [MaxLength(30)]
        public string Destination { get; set; }

        public int ServiceId { get; internal set; }


    }
}