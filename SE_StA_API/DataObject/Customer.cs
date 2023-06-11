using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Customer {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string FirstName{ get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string LastName{ get; set; }

        [MaxLength(50)]
        public string BirthDay { get; set; }
    }
}