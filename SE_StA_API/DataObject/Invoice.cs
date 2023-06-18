using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Invoice {
        [Key]
        public int InvoiceId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Number { get; set; }

        public List<Booking> bookings {get; internal set;}
    }
}