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

        public override bool Equals(object? obj)
        {
            if (obj == null || (obj is BundlesResponseModel))
            {
                return false;
            }
            List<ServicesResponseModel> servicesIds = ((BundlesResponseModel)obj).ServicesIds;
            if (servicesIds.Count != this.ServicesIds.Count)
            {
                return false;
            }
            for(int i=0; i<servicesIds.Count; i++)
            {
                if (!servicesIds[i].Equals(this.ServicesIds[i]))
                {
                    return false;
                }
            }

            return obj is BundlesResponseModel model &&
                Id == model.Id &&
                Name == model.Name &&
                Type == model.Type &&
                Price == model.Price &&
                Measure == model.Measure &&
                Duration == model.Duration;
        }
    }
}
