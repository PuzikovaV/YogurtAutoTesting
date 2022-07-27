using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogurtAutoTesting.Models.Request
{
    public class CleaningObjectRequestModel
    {
        [JsonProperty("numberOfRooms")]
        public int NumberOfRooms { get; set; }

        [JsonProperty("numberOfBathrooms")]
        public int NumberOfBathrooms { get; set; }

        [JsonProperty("square")]
        public int Square { get; set; }

        [JsonProperty("numberOfWindows")]
        public int NumberOfWindows { get; set; }

        [JsonProperty("numberOfBalconies")]
        public int NumberOfBalconies { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("clientId")]
        public int ClientId { get; set; }
    }
}
