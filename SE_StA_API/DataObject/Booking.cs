using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Booking {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Number { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string Date { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public double TotalPrice { get; set; }

        public int CustomerId { get; internal set; }

        public int InvoiceId { get; internal set; }
        
        public int PaymentMethodId { get; internal set; }

        public int StatusId { get; internal set; }

        public List<BookingPositionService> bookingPositionServices {get;internal set;}

        public List<BookingPositionRoom> bookingPositionRooms {get;internal set;}

    }
}