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

        [JsonPropertyName("roomType")]
        public int RoomType { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("measure")]
        public int Measure { get; set; }

        [JsonPropertyName("services")]
        public List<ServicesResponseModel> Services { get; set; }

        [JsonPropertyName("duration")]
        public double Duration { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is BundlesResponseModel))
            {
                return false;
            }
            List<ServicesResponseModel> services = ((BundlesResponseModel)obj).Services;
            if (services.Count != this.Services.Count)
            {
                return false;
            }
            for(int i=0; i<services.Count; i++)
            {
                if (!services[i].Equals(this.Services[i]))
                {
                    return false;
                }
            }

            return obj is BundlesResponseModel model &&
                Id == model.Id &&
                Name == model.Name &&
                Type == model.Type &&
                RoomType == model.RoomType &&
                Price == model.Price &&
                Measure == model.Measure &&
                Duration == model.Duration;
        }
    }
}
