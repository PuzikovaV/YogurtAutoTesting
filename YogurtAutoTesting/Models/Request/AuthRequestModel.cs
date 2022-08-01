using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Request
{
    public class AuthRequestModel
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
