using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.HttpClients
{
    public class ServicesClient
    {
        public HttpContent AddService(ServicesRequestModel model, string token, HttpStatusCode expected)
        {
            string json = JsonSerializer.Serialize(model);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Urls.Services),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;

            Assert.AreEqual(expected, actual);

            return response.Content;
        }

        public HttpContent GetServiceById(int id, string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Urls.Services}/{id}")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;

            Assert.AreEqual(expected, actual);

            return response.Content;

        }

        public HttpContent GetAllServices(string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Urls.Services)
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);

            return response.Content;
        }
        public void DeleteServiceById(int id, string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{Urls.Services}/{id}")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);
        }

        public void UpdateServiceById(ServicesRequestModel model, int id, string token, HttpStatusCode expected)
        {
            string json = JsonSerializer.Serialize(model);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{Urls.Services}/{id}"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);
        }
    }
}
