using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Request
{
    public class UpdateOrderRequestModel
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }

        [JsonPropertyName("bundlesIds")]
        public List<int> BundlesIds { get; set; }

        [JsonPropertyName("servicesIds")]
        public List<int> ServicesIds { get; set; }

        [JsonPropertyName("cleanersBandIds")]
        public List<int> CleanersBandIds { get; set; }
    }
}
