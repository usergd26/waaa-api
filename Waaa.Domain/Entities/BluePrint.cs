namespace Waaa.Domain.Entities
{
    public class BluePrint
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
