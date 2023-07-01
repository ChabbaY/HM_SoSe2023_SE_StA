using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class ServiceType {
        [Key]
        public int ServiceTypeId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public double DefaultPrice { get; set; }


    }
}