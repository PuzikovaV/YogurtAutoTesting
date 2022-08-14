using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.HttpClients
{
    public class BundlesClient
    {
        public HttpContent CreateBundles(BundlesRequestModel model, string token, HttpStatusCode expected)
        {
            string json = JsonSerializer.Serialize(model);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Urls.Bundles),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);
            return response.Content;
        }
        public HttpContent GetById(int id, string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Urls.Bundles}/{id}")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);

            return response.Content;
        }

        public HttpContent GetAllBundels(string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Urls.Bundles}")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);

            return response.Content;
        }

        public void UpdateBundelsById(BundlesRequestModel model, int id, string token, HttpStatusCode expected)
        {
            string json = JsonSerializer.Serialize(model);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{Urls.Bundles}/{id}"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);
        }

        public void DeleteBundleById(int id, string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{Urls.Bundles}/{id}")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);
        }

        public HttpContent GetBundlesAdditionalServicesById(int id, string token, HttpStatusCode expected)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Urls.Bundles}/{id}/additional-services")
            };
            HttpResponseMessage response = client.Send(message);
            HttpStatusCode actual = response.StatusCode;
            Assert.AreEqual(expected, actual);

            return response.Content;
        }
    }
}
