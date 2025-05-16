namespace KoperasiTentera.Models
{
    public class User
    {
        public int Id { get; set; }
        public string ICNNumber { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public bool IsMobileVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool HasAcceptedPolicy { get; set; }
        public string PinNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
