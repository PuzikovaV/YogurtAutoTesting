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
        //GetAllTests
        public List<ClientResponseModel> GetAllClientsByClientIdByAdminTest(string token, List<ClientResponseModel> expected)
        {
            HttpContent httpContent = _clientsClient.GetAllClientsByAdmin(token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;
            List<ClientResponseModel> actual = JsonSerializer.Deserialize<List<ClientResponseModel>>(content);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }
        public List<ClientResponseModel> GetAllClientsDoesNotContainOneByAdminTest(string token, ClientResponseModel model)
        {
            HttpContent httpContent = _clientsClient.GetAllClientsByAdmin(token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;
            List<ClientResponseModel> actual = JsonSerializer.Deserialize<List<ClientResponseModel>>(content);
            CollectionAssert.DoesNotContain(actual, model);
            return actual;
        }
        public void GetAllClientsWhenAdminDoNotAuthorizeTest(string token)
        {
            _clientsClient.GetAllClientsByAdmin(token, HttpStatusCode.Forbidden);
        }
        public void GetAllClientsWhenUnauthorizedTest(string token)
        {
            _clientsClient.GetAllClientsByAdmin(token, HttpStatusCode.Unauthorized);
        }
        //Delete Tests
        public void DeleteClientByAdminTest(int id, string token)
        {
            _clientsClient.DeleteClient(id, token, HttpStatusCode.NoContent);
        }
        public void DoNotDeleteClientTest(int id, string token)
        {
            _clientsClient.DeleteClient(id, token, HttpStatusCode.Forbidden);
        }
        public void DeleteClientWithWrongIdTest(int id, string token)
        {
            _clientsClient.DeleteClient(id, token, HttpStatusCode.BadRequest);
        }
        public void DeleteClientWhenUnauthorizeTest(int id, string token)
        {
            _clientsClient.DeleteClient(id, token, HttpStatusCode.Unauthorized);
        }
        //Update Tests
        public void UpdateClientById(int id, string token, UpdateClientRequestModel model)
        {
            _clientsClient.UpdateClient(model, id, token, HttpStatusCode.NoContent);
        }
        public void UpdateClientByIdWhenUserUnauthorizeTest(int id, string token, UpdateClientRequestModel model)
        {
            _clientsClient.UpdateClient(model, id, token, HttpStatusCode.Unauthorized);
        }
        public void UpdateClientByIdIsWrongTest(int id, string token, UpdateClientRequestModel model)
        {
            _clientsClient.UpdateClient(model, id, token, HttpStatusCode.UnprocessableEntity);
        }
        public void UpdateClientByIdByCleanerTest(int id, string token, UpdateClientRequestModel model)
        {
            _clientsClient.UpdateClient(model, id, token, HttpStatusCode.Forbidden);
        }
        //
        public List<CommentsByClientResponseModel> GetCommentsByClientIdTest(int id, string token, List<CommentsByClientResponseModel> expected)
        {
            HttpContent content = _clientsClient.GetAllCommentsByClientId(id, token, HttpStatusCode.OK);
            List<CommentsByClientResponseModel> actual = JsonSerializer.Deserialize<List<CommentsByClientResponseModel>>(content.ReadAsStringAsync().Result);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }
        public List<CommentsByCleanerResponseModel> GetAllCommentsAboutClientByClientId(int id, string token, List<CommentsByCleanerResponseModel> expected)
        {
            HttpContent content = _clientsClient.GetAllCommentsAboutClientByClientId(id, token, HttpStatusCode.OK);
            List<CommentsByCleanerResponseModel> actual = JsonSerializer.Deserialize<List<CommentsByCleanerResponseModel>>(content.ReadAsStringAsync().Result);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }
        public List<OrdersResponseModel> GetAllClientOrdersByIdTest(int id, string token, List<OrdersResponseModel> expected)
        {
            HttpContent content = _clientsClient.GetAllOrdersByClientId(id, token, HttpStatusCode.OK);
            List<OrdersResponseModel> actual = JsonSerializer.Deserialize<List<OrdersResponseModel>>(content.ReadAsStringAsync().Result);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }
    }
}
