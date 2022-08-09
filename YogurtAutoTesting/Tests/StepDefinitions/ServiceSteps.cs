﻿using System.Net;
using System.Text.Json;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Tests.StepDefinitions
{
    public class ServiceSteps
    {
        private ServicesClient _servicesClient;

        public ServiceSteps()
        {
            _servicesClient = new ServicesClient();
        }

        public int CreateServiceTest(ServicesRequestModel model, string token)
        {
            HttpContent httpContent = _servicesClient.AddService(model, token, HttpStatusCode.Created);
            string content = httpContent.ReadAsStringAsync().Result;
            int id = Convert.ToInt32(content);

            Assert.IsTrue(id > 0);
            return id;
        }

        public ServicesResponseModel GetServiceByIdTest(int id, string token, ServicesResponseModel expected)
        {
            HttpContent httpContent = _servicesClient.GetServiceById(id, token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;
            ServicesResponseModel actual = JsonSerializer.Deserialize<ServicesResponseModel>(content);

            Assert.AreEqual(expected, actual);

            return actual;
        }
    }
}
