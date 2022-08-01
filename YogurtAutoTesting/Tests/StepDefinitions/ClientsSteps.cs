using System.Net;
using System.Text.Json;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Tests.StepDefinitions
{
    public class ClientsSteps
    {
        private ClientsClient _clientsClient;
        public ClientsSteps()
        {
            _clientsClient = new ClientsClient();
        }

        public ClientResponseModel GetClientByIdTest(int id, string token, ClientResponseModel expected)
        {
            HttpContent httpContent = _clientsClient.GetClientById(id, token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;

            ClientResponseModel actual = JsonSerializer.Deserialize<ClientResponseModel>(content);

            Assert.AreEqual(expected, actual);

            return actual;
        }

        public List<ClientResponseModel> GetClientsByClientIdByAdminTest(int id, string token, List<ClientResponseModel> expected)
        {
            HttpContent httpContent = _clientsClient.GetClientById(id, token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;

            List<ClientResponseModel> actual = JsonSerializer.Deserialize<List<ClientResponseModel>>(content);

            CollectionAssert.AreEquivalent(expected, actual);

            return actual;
        }

        public void DeleteClientByAdminTest(int id, string token)
        {
            _clientsClient.DeleteClient(id, token, HttpStatusCode.NoContent);
        }
    }
}
