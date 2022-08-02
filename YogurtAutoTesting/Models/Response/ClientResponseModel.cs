using System.Text.Json.Serialization;

namespace YogurtAutoTesting.Models.Response
{
    public class ClientResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is ClientResponseModel model &&
                Id == model.Id &&
                FirstName == model.FirstName &&
                LastName == model.LastName &&
                RegistrationDate == model.RegistrationDate &&
                Email == model.Email &&
                Phone == model.Phone &&
                BirthDate == model.BirthDate;
        } 
    }
}
