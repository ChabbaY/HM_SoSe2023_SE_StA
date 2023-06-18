using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class ContactAddress {
        [Key]
        public int ContactId { get; set; }
         [Key]
        public int AddressId { get; set; }
    }
}