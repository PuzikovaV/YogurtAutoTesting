using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.HttpClients
{
    public class AuthClient
    {
        public HttpResponseMessage Authorize(AuthRequestModel model)
        {
            string json = JsonSerializer.Serialize(model);
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Urls.Auth),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            return client.Send(message);
        }
    }
}
