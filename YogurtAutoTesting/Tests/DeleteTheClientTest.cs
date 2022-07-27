using System.Net;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests
{
    public class DeleteTheClientTest
    {
        private AuthClient _authClient = new AuthClient();
        private ClientsClient _clientsClient = new ClientsClient();
        [Test]
        public void ClientIsDeleted_WhenIdIsCorrect_ShouldDeleteTheClient()
        {
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "Admin@gmail.com",
                Password = "qwerty12345",
            };
            HttpStatusCode expectedAuthCode = HttpStatusCode.OK;

            HttpResponseMessage authResponse = _authClient.Authorize(authModel);
            HttpStatusCode actualAuthCode = authResponse.StatusCode;
            string actualToken = authResponse.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(expectedAuthCode, actualAuthCode);
            Assert.NotNull(actualToken);

            string token = actualToken;

            int clientId = 37;

            HttpStatusCode expectedCode = HttpStatusCode.NoContent;
            HttpStatusCode actualCode = _clientsClient.DeleteClient(clientId, token);

            Assert.AreEqual(expectedCode, actualCode);
        }

    }
}
