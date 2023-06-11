using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class PaymentMethod {
        [Key]
        public int PaymentMethodId { get; set; }

    }
}