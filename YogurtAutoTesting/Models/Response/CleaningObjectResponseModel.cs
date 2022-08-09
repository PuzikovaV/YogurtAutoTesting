using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Response
{
    public class CleaningObjectResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("clientId")]
        public int ClientId { get; set; }

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

        public override bool Equals(object? obj)
        {
            return obj is CleaningObjectResponseModel model &&
                   Id == model.Id &&
                   ClientId == model.ClientId &&
                   NumberOfRooms == model.NumberOfRooms &&
                   NumberOfBathrooms == model.NumberOfBathrooms &&
                   Square == model.Square &&
                   NumberOfWindows == model.NumberOfWindows &&
                   NumberOfBalconies == model.NumberOfBalconies &&
                   Address == model.Address &&
                   District == model.District;
        }
    }
}
