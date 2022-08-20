using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Request
{
    public class BundlesRequestModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("roomType")]
        public int RoomType { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("measure")]
        public int Measure { get; set; }

        [JsonPropertyName("servicesIds")]
        public List<int> ServicesIds { get; set; }

        [JsonPropertyName("duration")]
        public double Duration { get; set; }
    }
}
