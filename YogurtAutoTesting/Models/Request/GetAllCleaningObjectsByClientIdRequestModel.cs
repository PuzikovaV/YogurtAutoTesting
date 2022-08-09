using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Request
{
    public class GetAllCleaningObjectsByClientIdRequestModel
    {
        [JsonPropertyName("clientId")]
        public int ClientId { get; set; }

    }
}
