namespace Waaa.Domain.Models
{
    public class WebinarRegistrations
    {
        public int RegistrationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int WebinarId { get; set; }
        public string WebinarName { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
