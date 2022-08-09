using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.HttpClients
{
    public class ClientsClient
    {
        public HttpContent RegisterClient(ClientRequestModel model, HttpStatusCode expectedCode)
        {
            string json = JsonSerializer.Serialize(model);

            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Urls.Clients),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actualCode = response.StatusCode;

            Assert.AreEqual(expectedCode, actualCode);

            return response.Content;
        }
        
        public HttpContent GetClientById(int id, string token, HttpStatusCode expectedCode)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Urls.Clients}/{id}")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actualCode = response.StatusCode;

            Assert.AreEqual(expectedCode, actualCode);

            return response.Content;
        }

        public HttpContent GetAllClientsByAdmin(string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Urls.Clients)
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;

            Assert.AreEqual(expected, actual);

            return response.Content;
        }

        public void DeleteClient(int id, string token, HttpStatusCode expectedCode)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{Urls.Clients}/{id}")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actualCode = response.StatusCode;

            Assert.AreEqual(expectedCode, actualCode);
        }

        public void UpdateClient(UpdateClientRequestModel model, int id, string token, HttpStatusCode expected)
        {
            string json = JsonSerializer.Serialize(model);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{Urls.Clients}/{id}"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;

            Assert.AreEqual(expected, actual);

        }
    }
}
