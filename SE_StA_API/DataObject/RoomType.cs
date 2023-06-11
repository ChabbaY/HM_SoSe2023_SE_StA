using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class RoomType {
        [Key]
        public int RoomTypeId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

         [Required]
         [MaxLength(50)]
         public double StandardPrice { get; set; }
    }
}