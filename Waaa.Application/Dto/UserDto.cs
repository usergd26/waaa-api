using System.Text.Json.Serialization;

namespace Waaa.Application.Dto
{
    public class UserDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
