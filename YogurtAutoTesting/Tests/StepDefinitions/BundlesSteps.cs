using System.Net;
using System.Text.Json;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Tests.StepDefinitions
{
    public class BundlesSteps
    {
        private BundlesClient _bundelsClient;
        public BundlesSteps()
        {
            _bundelsClient = new BundlesClient();
        }

        public int CreateBundleTest(BundlesRequestModel model, string token)
        {
            HttpContent content = _bundelsClient.CreateBundles(model, token, HttpStatusCode.Created);
            int id = Convert.ToInt32(content.ReadAsStringAsync().Result);

            Assert.IsTrue(id > 0);

            return id;
        }

        public BundlesResponseModel GetBundleByIdTest(int id, string token, BundlesResponseModel expected)
        {
            HttpContent httpContent = _bundelsClient.GetById(id, token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;

            BundlesResponseModel actual = JsonSerializer.Deserialize<BundlesResponseModel>(content);
            Assert.AreEqual(expected, actual);

            return actual;
        }

        public List<BundlesResponseModel> GetAllBundlesTest(string token, List<BundlesResponseModel> expected)
        {
            HttpContent httpContent = _bundelsClient.GetAllBundels(token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;

            List<BundlesResponseModel> actual = JsonSerializer.Deserialize<List<BundlesResponseModel>>(content);
            CollectionAssert.AreEquivalent(expected, actual);

            return actual;
        }

        public void UpdateBundleByIdTest(BundlesRequestModel model, int id, string token)
        {
            _bundelsClient.UpdateBundelsById(model, id, token, HttpStatusCode.NoContent);
        }

        public void DeleteBundleByIdTest(int id, string token)
        {
            _bundelsClient.DeleteBundleById(id, token, HttpStatusCode.NoContent);
        }

        public List<ServicesResponseModel> GetBundlesAdditionalServicesByIdTest(int id, string token, List<ServicesResponseModel> expected)
        {
            HttpContent httpContent = _bundelsClient.GetBundlesAdditionalServicesById(id, token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;

            List<ServicesResponseModel> actual = JsonSerializer.Deserialize<List<ServicesResponseModel>>(content);
            CollectionAssert.AreEquivalent(expected, actual);

            return actual;
        }
    }
}
