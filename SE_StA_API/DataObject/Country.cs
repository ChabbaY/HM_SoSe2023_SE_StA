using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Country {
        [Key]
        public int CountryId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Language { get; set; }

        [Required]
        [StringLength(2)]
        public string Iso2 { get; set; }


        
    }
}