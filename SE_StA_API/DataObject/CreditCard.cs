using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class CreditCard {
        [Key]
        public int CreditCardId { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        public string CardNumber { get; set; }

        public int PaymentMethod { get; internal set; }
    }
}