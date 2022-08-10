using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Request
{
    public class UpdateCleaningObjectRequestModel
    {
        [JsonPropertyName("numberOfRooms")]
        public int NumberOfRooms { get; set; }

        [JsonPropertyName("numberOfBathrooms")]
        public int NumberOfBathrooms { get; set; }

        [JsonPropertyName("square")]
        public int Square { get; set; }

        [JsonPropertyName("numberOfWindows")]
        public int NumberOfWindows { get; set; }

        [JsonPropertyName("numberOfBalconies")]
        public int NumberOfBalconies { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("district")]
        public int District { get; set; }
    }
}
