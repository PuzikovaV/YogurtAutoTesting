using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Response
{
    public class ServicesResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("duration")]
        public double Duration { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ServicesResponseModel model &&
                Id == model.Id &&
                Name == model.Name &&
                Price == model.Price &&
                Unit == model.Unit &&
                Duration == model.Duration;
        }
    }
}
