using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Service {
        [Key]
        public int ServiceId { get; set; }

        public int ServiceTypeId { get; internal set; }

        public List<Flight> flights { get; internal set; }

        public List<RentalCar> rentalCars { get; internal set; }

        public List<Wellness> wellnesses {get;internal set;}

        public List<BookingPositionService> bookingPositionServices {get;internal set;}
    }
}