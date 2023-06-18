using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class PaymentMethod {
        [Key]
        public int PaymentMethodId { get; set; }

        public int CustomerId { get; internal set; }

        public List<Booking> bookings { get; internal set; }

        public List<BankAccount> bankAccounts { get; internal set; }

        public List<Cash> cashes { get; internal set; }

        public List<CreditCard> creditCards { get; internal set; }

    }
}