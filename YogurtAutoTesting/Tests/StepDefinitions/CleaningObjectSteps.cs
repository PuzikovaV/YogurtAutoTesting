using System.Net;
using System.Text.Json;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Tests.StepDefinitions
{
    public class CleaningObjectSteps
    {
        private CleaningObjectClient _cleaningObjectClient;

        public CleaningObjectSteps()
        {
            _cleaningObjectClient = new CleaningObjectClient();
        }

        public int AddCleaningObjectTest(CleaningObjectRequestModel model, string token)
        {
            HttpContent httpContent = _cleaningObjectClient.CreateACleaningObject(model, HttpStatusCode.Created, token);
            string content = httpContent.ReadAsStringAsync().Result;

            Assert.NotNull(content);

            int id = Convert.ToInt32(content);

            Assert.IsTrue(id > 0);

            return id;
        }

        public CleaningObjectResponseModel GetCleaningObjectByIdTest(int id, string token, CleaningObjectResponseModel expectedModel)
        {
            HttpContent httpContent = _cleaningObjectClient.GetCleaningObjectById(id, token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;

            CleaningObjectResponseModel actualModel = JsonSerializer.Deserialize<CleaningObjectResponseModel>(content);

            Assert.AreEqual(expectedModel, actualModel);

            return actualModel;
        }

        public List<CleaningObjectResponseModel> GetAllCleaningObjectsByClientIdTest(int id, string token, List<CleaningObjectResponseModel> expectedModel)
        {
            HttpContent httpContent = _cleaningObjectClient.GetAllCleaningObjectsByClientId(id, token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;

            List<CleaningObjectResponseModel> actualModel = JsonSerializer.Deserialize<List<CleaningObjectResponseModel>>(content);

            CollectionAssert.AreEquivalent(expectedModel, actualModel);

            return actualModel;
        }

        public void DeleteCleaningObjectTest(int id, string token)
        {
            _cleaningObjectClient.DeleteCleaningIbject(id, token, HttpStatusCode.NoContent);
        }
    }
}
