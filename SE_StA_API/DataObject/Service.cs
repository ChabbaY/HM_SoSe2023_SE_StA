using System.ComponentModel.DataAnnotations;

namespace SE_StA_API.DataObject {
    public class Service {
        [Key]
        public int ServiceId { get; set; }

        public int ServiceTypeId { get; internal set; }

    }
}