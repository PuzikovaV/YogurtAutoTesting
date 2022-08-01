using System.Net;
using System.Text.Json;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class RegistrationOfAClient
    {
        private AuthorizationSteps _authorizationSteps = new AuthorizationSteps();

        [Test]
        public void ClientCreate_WhenClientModelIsCorrect_ShouldCreateClient()
        {

            ClientRequestModel clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "thebestKostya666",
                ConfirmPassword = "thebestKostya666",
                Email = "kostik08@gmail.com",
                Phone = "89996662233"
            };

            int id = _authorizationSteps.RegisterClient(clientRequest);

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = clientRequest.Email,
                Password = clientRequest.Password,
            };

            string token = _authorizationSteps.Authorize(authModel);

            ClientResponseModel expectedClient = new ClientResponseModel()
            {
                Id = id,
                FirstName = clientRequest.FirstName,
                LastName = clientRequest.LastName,
                RegistrationDate = new DateTime(),
                Email = clientRequest.Email,
                Phone = clientRequest.Phone,
                BirthDate = clientRequest.BirthDate
            };

            HttpContent content = _clientsClient.GetClientById(id, token, HttpStatusCode.OK);
            ClientResponseModel actualClient = JsonSerializer.Deserialize<ClientResponseModel>(content.ReadAsStringAsync().Result);

            Assert.AreEqual(expectedClient, actualClient);
        }

        [Test]
        public void CreateClient_WhenPasswordLessThenFourSymbols_ShouldNotRegistrate()
        {
            ClientRequestModel clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "the8",
                ConfirmPassword = "the8",
                Email = "kostik888@gmail.com",
                Phone = "89996662233"
            };
            HttpContent response = _clientsClient.RegisterClient(clientRequest, HttpStatusCode.UnprocessableEntity);
        }

        [Test]
        public void CreateClient_WhenPasswordLessThenSevenSymbols_ShouldNotRegistrate()
        {
            ClientRequestModel clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "1234567",
                ConfirmPassword = "1234567",
                Email = "kostik888@gmail.com",
                Phone = "89996662233"
            };
            HttpContent response = _clientsClient.RegisterClient(clientRequest, HttpStatusCode.UnprocessableEntity);
        }

        [Test]
        public void CreateClient_WhenEnterExistsEmail_ShouldNotRegistrate()
        {
            ClientRequestModel clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "12345678",
                ConfirmPassword = "12345678",
                Email = "kostik@gmail.com",
                Phone = "89996662233"
            };
            HttpContent response = _clientsClient.RegisterClient(clientRequest, HttpStatusCode.UnprocessableEntity);
        }

    }
}
