using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Customer {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string FirstName{ get; set; }
       
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string LastName{ get; set; }

        [MaxLength(10)]
        public string DateOfBirth { get; set; }

        [MaxLength(10)]
        public string Number { get; set; }

        public List<Booking> bookings { get; internal set; }

        public List<PaymentMethod> paymentMethods { get; internal set; }

        public int ContactId { get; internal set; }

        public int UserId { get; internal set; }
    }
}