using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Status {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Name { get; set; }


    }
}