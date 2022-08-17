using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Response
{
    internal class CommentsAboutResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is CommentsAboutResponseModel model &&
                Id == model.Id &&
                Summary == model.Summary &&
                Rating == model.Rating;
        }
    }
}
