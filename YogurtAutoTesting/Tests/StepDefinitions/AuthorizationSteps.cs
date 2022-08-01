using System.Net;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
.

namespace YogurtAutoTesting.Tests.StepDefinitions
{
    public class AuthorizationSteps
    {
        private AuthClient _authClient = new AuthClient();
        private ClientsClient _clientsClient = new ClientsClient();
        public string Authorize(AuthRequestModel model)
        {
            HttpContent authContent = _authClient.Authorize(model, HttpStatusCode.OK);
            string actualToken = authContent.ReadAsStringAsync().Result;

            Assert.NotNull(actualToken);

            return actualToken;
        }

        public int RegisterClient(ClientRequestModel model)
        {
            HttpContent clientContent = _clientsClient.RegisterClient(model, HttpStatusCode.Created);
            int id = Convert.ToInt32(clientContent.ReadAsStringAsync().Result);

            Assert.IsTrue(id>0);

            return id;
        }

        
    }
}
