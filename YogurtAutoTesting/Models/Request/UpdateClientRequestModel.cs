using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Request
{
    public class UpdateClientRequestModel
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
}
