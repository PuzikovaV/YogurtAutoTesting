using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Response
{
    public class CommentsResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("clientId")]
        public int ClientId { get; set; }

        [JsonPropertyName("cleanerId")]
        public int CleanerId { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }
    }
}
