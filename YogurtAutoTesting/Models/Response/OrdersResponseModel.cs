using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Response
{
    public class OrdersResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("client")]
        public ClientResponseModel Client { get; set; }

        [JsonPropertyName("cleaningObject")]
        public CleaningObjectResponseModel CleaningObject { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("bundles")]
        public List<BundlesResponseModel> Bundles { get; set; }

        [JsonPropertyName("services")]
        public List<ServicesResponseModel> Services { get; set; }

        [JsonPropertyName("cleanersBand")]
        public List<CleanerResponseModel> CleanersBand { get; set; }

        [JsonPropertyName("comments")]
        public List<CommentsResponseModel> Comments { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || (obj is OrdersResponseModel))
            {
                return false;
            }
            ClientResponseModel client = ((OrdersResponseModel)obj).Client;
            if (!client.Equals(this.Client))
            {
                return false;
            }

            if (obj == null || (obj is OrdersResponseModel))
            {
                return false;
            }
            CleaningObjectResponseModel cleaningObject = ((OrdersResponseModel)obj).CleaningObject;
            if (!cleaningObject.Equals(this.CleaningObject))
            {
                return false;
            }

            if (obj == null || (obj is OrdersResponseModel))
            {
                return false;
            }
            List<BundlesResponseModel> bundles = ((OrdersResponseModel)obj).Bundles;
            if (bundles.Count != this.Bundles.Count)
            {
                return false;
            }
            for (int i = 0; i < bundles.Count; i++)
            {
                if (!bundles[i].Equals(this.Bundles[i]))
                {
                    return false;
                }
            }

            if (obj == null || (obj is OrdersResponseModel))
            {
                return false;
            }
            List<ServicesResponseModel> services = ((OrdersResponseModel)obj).Services;
            if (services.Count != this.Services.Count)
            {
                return false;
            }
            for (int i = 0; i < services.Count; i++)
            {
                if (!services[i].Equals(this.Services[i]))
                {
                    return false;
                }
            }

            if (obj == null || (obj is OrdersResponseModel))
            {
                return false;
            }
            List<CleanerResponseModel> cleanersBand = ((OrdersResponseModel)obj).CleanersBand;
            if (cleanersBand.Count != this.CleanersBand.Count)
            {
                return false;
            }
            for (int i = 0; i < cleanersBand.Count; i++)
            {
                if (!cleanersBand[i].Equals(this.CleanersBand[i]))
                {
                    return false;
                }
            }

            if (obj == null || (obj is OrdersResponseModel))
            {
                return false;
            }
            List<CommentsResponseModel> comments = ((OrdersResponseModel)obj).Comments;
            if (comments.Count != this.Comments.Count)
            {
                return false;
            }
            for (int i = 0; i < comments.Count; i++)
            {
                if (!comments[i].Equals(this.Comments[i]))
                {
                    return false;
                }
            }
            return obj is OrdersResponseModel model &&
                Id == model.Id &&
                Status == model.Status &&
                StartTime == model.StartTime &&
                EndTime == model.EndTime &&
                UpdateTime == model.UpdateTime &&
                Price == model.Price;
        }
    }
}
