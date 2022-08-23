using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Response
{
    public class CommentsByCleanerResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("clientId")]
        public string ClientId { get; set; }

        [JsonPropertyName("cleanerId")]
        public int CleanerId { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CommentsByCleanerResponseModel model &&
                Id == model.Id &&
                Summary == model.Summary &&
                ClientId == model.ClientId &&
                CleanerId == model.CleanerId &&
                OrderId == model.OrderId &&
                Rating == model.Rating;
        }
    }
}
