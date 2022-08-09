using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Request
{
    public class ServicesRequestModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("duration")]
        public double Duration { get; set; }
    }
}
