using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Wellness {
        [Key]
        public int WellnessId { get; set; }

        [MaxLength(10)]
        public string Duration { get;  set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Name { get; set; }

        public int ServiceId { get; internal set; }
    }
}