using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class ContactAddress {
        [Key]
        public int ContactAddressId { get; set; }
        
        public int ContactId { get; set; }
        
        public int AddressId { get; set; }
    }
}