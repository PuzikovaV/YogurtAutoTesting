using System.Text.Json.Serialization;


namespace YogurtAutoTesting.Models.Response
{
    public class BundlesResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("measure")]
        public int Measure { get; set; }

        [JsonPropertyName("servicesIds")]
        public List<ServicesResponseModel> ServicesIds { get; set; }

        [JsonPropertyName("duration")]
        public double Duration { get; set; }
    }
}
