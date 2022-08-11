using System.Net;
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
    }
}
