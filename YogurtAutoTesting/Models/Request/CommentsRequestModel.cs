using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Request
{
    public class CommentsRequestModel
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }
    }
}
