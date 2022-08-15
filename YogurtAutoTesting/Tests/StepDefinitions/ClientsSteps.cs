using System.Net;
using System.Text.Json;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
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

        public DateTime GetRegisterDate(int id, string token)
        {
            HttpContent httpContent = _clientsClient.GetClientById(id, token, HttpStatusCode.OK);
            ClientResponseModel content = JsonSerializer.Deserialize<ClientResponseModel>(httpContent.ReadAsStringAsync().Result);
            DateTime registerTime = content.RegistrationDate;
            return registerTime;
        }

        public List<ClientResponseModel> GetAllClientsByClientIdByAdminTest(string token, List<ClientResponseModel> expected)
        {
            HttpContent httpContent = _clientsClient.GetAllClientsByAdmin(token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;
            List<ClientResponseModel> actual = JsonSerializer.Deserialize<List<ClientResponseModel>>(content);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }

        public void DeleteClientByAdminTest(int id, string token)
        {
            _clientsClient.DeleteClient(id, token, HttpStatusCode.NoContent);
        }

        public void UpdateClientById(int id, string token, UpdateClientRequestModel model)
        {
            _clientsClient.UpdateClient(model, id, token, HttpStatusCode.NoContent);
        }

        public List<CommentsResponseModel> GetCommentsByClientIdTest(int id, string token, List<CommentsResponseModel> expected)
        {
            HttpContent content = _clientsClient.GetAllCommentsByClientId(id, token, HttpStatusCode.OK);
            List<CommentsResponseModel> actual = JsonSerializer.Deserialize<List<CommentsResponseModel>>(content.ReadAsStringAsync().Result);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }

        public List<CommentsResponseModel> GetAllCommentsAboutClientByClientId(int id, string token, List<CommentsResponseModel> expected)
        {
            HttpContent content = _clientsClient.GetAllCommentsAboutClientByClientId(id, token, HttpStatusCode.OK);
            List<CommentsResponseModel> actual = JsonSerializer.Deserialize<List<CommentsResponseModel>>(content.ReadAsStringAsync().Result);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }
        
    }
}
