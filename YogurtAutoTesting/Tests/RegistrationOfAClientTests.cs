using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Tests
{
    public class RegistrationOfAClient
    {
        private ClientsClient _clientsClient = new ClientsClient();
        private AuthClient _authClient = new AuthClient();
        [Test]
        public void ClientCreate_WhenClientModelIsCorrect_ShouldCreateClient()
        {

            /*ClientRequestModel clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "thebestKostya666",
                ConfirmPassword = "thebestKostya666",
                Email = "kostik08@gmail.com",
                Phone = "89996662233"
            };
            HttpStatusCode expectedRegCode = HttpStatusCode.Created;

            HttpResponseMessage response = _clientsClient.RegisterClient(clientRequest);
            HttpStatusCode actualRegCode = response.StatusCode;
            string id = response.Content.ReadAsStringAsync().Result;
            int? actualId = Convert.ToInt32(id);

            Assert.AreEqual(expectedRegCode, actualRegCode);
            Assert.NotNull(actualId);
            Assert.IsTrue(actualId > 0);

            int clientId = (int)actualId;*/

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "kostik0@gmail.com",
                Password = "thebestKostya666",
            };
            HttpStatusCode expectedAuthCode = HttpStatusCode.OK;

            HttpResponseMessage authResponse = _authClient.Authorize(authModel);
            HttpStatusCode actualAuthCode = authResponse.StatusCode;
            string actualToken = authResponse.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(expectedAuthCode, actualAuthCode);
            Assert.NotNull(actualToken);

            string token = actualToken;

            ClientResponseModel expectedClient = new ClientResponseModel()
            {
                Id = 41,
                FirstName = "Константин",
                LastName = "Придуманный",
                RegistrationDate = new DateTime(),
                Email = "kostik0@gmail.com",
                Phone = "89996662233",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00)
            };

            HttpContent content = _clientsClient.GetClientById(41, token, HttpStatusCode.OK);
            string notJson = content.ReadAsStringAsync().Result;
            ClientResponseModel actualClient = JsonSerializer.Deserialize<ClientResponseModel>(notJson);

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
            HttpStatusCode expectedRegCode = HttpStatusCode.UnprocessableEntity;

            HttpResponseMessage response = _clientsClient.RegisterClient(clientRequest);
            HttpStatusCode actualRegCode = response.StatusCode;

            Assert.AreEqual(expectedRegCode, actualRegCode);
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
            HttpStatusCode expectedRegCode = HttpStatusCode.UnprocessableEntity;

            HttpResponseMessage response = _clientsClient.RegisterClient(clientRequest);
            HttpStatusCode actualRegCode = response.StatusCode;

            Assert.AreEqual(expectedRegCode, actualRegCode);
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
            HttpStatusCode expectedRegCode = HttpStatusCode.UnprocessableEntity;

            HttpResponseMessage response = _clientsClient.RegisterClient(clientRequest);
            HttpStatusCode actualRegCode = response.StatusCode;

            Assert.AreEqual(expectedRegCode, actualRegCode);
        }

    }
}
