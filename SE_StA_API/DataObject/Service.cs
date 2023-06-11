using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Service {
        [Key]
        public int ServiceId { get; set; }

        public int ServiceType { get; internal set; }

        public List<Flight> positions { get; internal set; }

        public List<RentalCar> rentalCars { get; internal set; }

        public List<Wellness> wellnesses {get;internal set;}

        public List<BookingPositionService> BookingPositionServices {get;internal set;}
    }
}