using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Request
{
    public class OrderRequestModel
    {
        [JsonPropertyName("cleaningObjectId")]
        public int CleaningObjectId { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("bundlesIds")]
        public List<int> BundlesIds { get; set; }

        [JsonPropertyName("servicesIds")]
        public List<int> ServicesIds { get; set; }
    }
}
