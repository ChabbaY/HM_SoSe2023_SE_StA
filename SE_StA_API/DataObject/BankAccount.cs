using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class BankAccount {
        [Key]
        public int BankAccountId { get; set; }

        [Required]
        [MinLength(22)]
        [MaxLength(22)]
        public string Iban { get; set; }

        public int PaymentMethodId { get; internal set; }

    }
}