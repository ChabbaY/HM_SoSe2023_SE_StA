using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class RentalCar {
        [Key]
        public int RentalCarId { get; set; }

        [MaxLength(30)]
        public string CarModel { get;  set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(2)]
        public string Seats { get; set; }
    }
}