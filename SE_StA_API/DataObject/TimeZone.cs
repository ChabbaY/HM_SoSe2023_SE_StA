using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class TimeZone {
        [Key]
        public int TimeZoneId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

         [Required]
         [MaxLength(50)]
         public int difUtc { get; set; }


         public List<CountryTimeZone> countryTimeZones {get; internal set;}

         public List<Address> addresses {get; internal set;}
    }
}