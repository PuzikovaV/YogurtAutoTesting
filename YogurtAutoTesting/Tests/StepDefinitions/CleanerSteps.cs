using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using System.Text.Json;

namespace YogurtAutoTesting.Tests.StepDefinitions
{
    public class CleanerSteps
    {
        private CleanerClient _cleanerClient;

        public CleanerSteps()
        {
            _cleanerClient = new CleanerClient();
        }

        public int CreateCleanerTest(CleanerRequestModel model)
        {
            HttpContent httpContent = _cleanerClient.RegisterCleaner(model, HttpStatusCode.Created);
            string content = httpContent.ReadAsStringAsync().Result;
            int cleanerId = Convert.ToInt32(content);

            Assert.IsTrue(cleanerId > 0);

            return cleanerId;
        }

        public CleanerResponseModel GetCleanerById(int id, string token, CleanerResponseModel expected)
        {
            HttpContent httpContent = _cleanerClient.GetCleanerById(id, token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;

            CleanerResponseModel actual = JsonSerializer.Deserialize<CleanerResponseModel>(content);

            Assert.AreEqual(expected, actual);

            return actual;
        }

        public List<GetAllCleanersResponseModel> GetAllCleanersByAdminTest(string token, List<GetAllCleanersResponseModel> expected)
        {
            HttpContent httpContent = _cleanerClient.GetAllCleaners(token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;

            List<GetAllCleanersResponseModel> actual = JsonSerializer.Deserialize<List<GetAllCleanersResponseModel>>(content);

            CollectionAssert.AreEquivalent(expected, actual);

            return actual;
        }

    }
}
