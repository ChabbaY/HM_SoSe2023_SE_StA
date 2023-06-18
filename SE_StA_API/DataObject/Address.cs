using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Address {
        [Key]
        public int AdressId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string Steet { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string HouseNumber { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public string PostalCode { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Town { get; set; }

        [MaxLength(20)]
        public string AddressAddition { get; set; }

        public int CountryId {get; internal set; }
        public int TimeZoneId {get; internal set; }

        public List<ContactAddress> contactAdresses {get; internal set;}

    }
}