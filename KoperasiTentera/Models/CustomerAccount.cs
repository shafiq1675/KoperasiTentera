using System.ComponentModel.DataAnnotations;

namespace KoperasiTentera.Models
{
    public class CustomerAccount
    {
        public string CustomerName { get; set; }
        public string IcNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public int TempPin { get; set; }
        public int PIN { get; set; }
        public string DeviceId { get; set; }
        public bool IsMobileVerified { get; set; }
    }
}
