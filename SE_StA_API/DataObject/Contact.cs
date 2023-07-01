using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Contact {
        [Key]
        public int ContactId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Salutation { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }
    }
}