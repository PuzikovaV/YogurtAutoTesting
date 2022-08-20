using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Response
{
    public class CleanerResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("dateOfStartWork")]
        public DateTime DateOfStartWork { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("services")]
        public List<ServicesResponseModel> Services { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is CleanerResponseModel))
            {
                return false;
            }
            List<ServicesResponseModel> services = ((CleanerResponseModel)obj).Services;
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
            //CollectionAssert.AreEquivalent(this.Services, ((CleanerResponseModel)obj).Services);
            return obj is CleanerResponseModel model &&
                Id == model.Id &&
                FirstName == model.FirstName &&
                LastName == model.LastName &&
                DateOfStartWork.Date == model.DateOfStartWork.Date &&
                BirthDate == model.BirthDate &&
                Phone == model.Phone &&
                Email == model.Email &&
                Rating == model.Rating;
        }
    }
}
