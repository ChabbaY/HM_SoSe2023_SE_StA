using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class CountryTimeZone {
        [Key]
        public int CountryTimeZoneId { get; set; }

        public int CountryId { get; internal set; }

        public int TimeZoneId { get; internal set; }

        
    }
}