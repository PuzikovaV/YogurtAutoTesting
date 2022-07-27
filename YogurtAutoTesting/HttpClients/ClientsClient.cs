﻿using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.HttpClients
{
    public class ClientsClient
    {
        public HttpResponseMessage RegisterClient(ClientRequestModel model)
        {
            string json = JsonSerializer.Serialize(model);

            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Urls.Clients),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            return client.Send(message);
        }
        public HttpStatusCode DeleteClient(int id, string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{Urls.Clients}/{id}")
            };
            HttpResponseMessage response = client.Send(message);

            return response.StatusCode;
            
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
    }
}
