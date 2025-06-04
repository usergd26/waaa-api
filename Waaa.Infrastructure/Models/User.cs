using System.Text.Json.Serialization;

namespace Waaa.Infrastructure.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
