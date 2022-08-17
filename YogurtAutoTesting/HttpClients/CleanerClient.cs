using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.HttpClients
{
    public class CleanerClient
    {
        public HttpContent RegisterCleaner(CleanerRequestModel model, HttpStatusCode expected)
        {
            string json = JsonSerializer.Serialize(model);
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Urls.Cleaners),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);

            return response.Content;
        }

        public HttpContent GetCleanerById(int id, string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Urls.Cleaners}/{id}")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);

            return response.Content;
        }

        public HttpContent GetAllCleaners(string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Urls.Cleaners),
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);

            return response.Content;
        }

        public void DeleteCleanerById(string token, int id, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{Urls.Cleaners}/{id}")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);
        }

        public void UpdateCleanerById(int id, string token, UpdateCleanerRequestModel model, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            string json = JsonSerializer.Serialize(model);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{Urls.Cleaners}/{id}"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);
        }

        public HttpContent GetAllCommentsByCleanerId(int id, string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Urls.Clients}/{id}/comments-by-cleaner")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);
            return response.Content;
        }

        public HttpContent GetAllOrdersByCleanerId(int id, string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Urls.Cleaners}/{id}/orders")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);
            return response.Content;
        }

        public HttpContent GetAllCommentsAboutCleanerByCleanerId(int id, string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Urls.Cleaners}/{id}/comments-about-cleaner")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);
            return response.Content;
        }
    }
}
