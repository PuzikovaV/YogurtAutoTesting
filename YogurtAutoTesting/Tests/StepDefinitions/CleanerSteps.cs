using System.Net;
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

        public List<CleanerResponseModel> GetAllCleanersByAdminTest(string token, List<CleanerResponseModel> expected)
        {
            HttpContent httpContent = _cleanerClient.GetAllCleaners(token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;
            List<CleanerResponseModel> actual = JsonSerializer.Deserialize<List<CleanerResponseModel>>(content);
            CollectionAssert.AreEquivalent(expected, actual);

            return actual;
        }

        public void DeleteCleanerByIdTest(int id, string token)
        {
            _cleanerClient.DeleteCleanerById(token, id, HttpStatusCode.NoContent);
        }
        public void UpdateCleanerByIdTest(int id, string token, UpdateCleanerRequestModel model)
        {
            _cleanerClient.UpdateCleanerById(id, token, model, HttpStatusCode.NoContent);
        }

        public List<CommentsResponseModel> GetAllCommentsAboutCleanerByCleanerId(int id, string token, List<CommentsResponseModel> expected)
        {
            HttpContent content = _cleanerClient.GetAllCommentsAboutCleanerByCleanerId(id, token, HttpStatusCode.OK);
            List<CommentsResponseModel> actual = JsonSerializer.Deserialize<List<CommentsResponseModel>>(content.ReadAsStringAsync().Result);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }

    }
}
