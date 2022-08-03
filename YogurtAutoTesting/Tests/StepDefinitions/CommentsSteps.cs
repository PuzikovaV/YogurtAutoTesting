using System.Net;
using System.Text.Json;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Tests.StepDefinitions
{
    public class CommentsSteps
    {
        private CommentsClient _commentsClient;
        
        public CommentsSteps()
        {
            _commentsClient = new CommentsClient();
        }

        public int AddCommentTest(CommentsRequestModel model, string token)
        {
            HttpContent content = _commentsClient.AddComment(model, token, HttpStatusCode.OK);
            int id = Convert.ToInt32(content.ReadAsStringAsync().Result);

            Assert.IsTrue(id > 0);

            return id;
        }

        public List<CommentsResponseModel> GetCommentsByClientIdTest (int id, string token, List<CommentsResponseModel> expected)
        {
            HttpContent content = _commentsClient.GetAllCommentsByClientId(id, token, HttpStatusCode.OK);
            List<CommentsResponseModel> actual = JsonSerializer.Deserialize<List<CommentsResponseModel>>(content.ReadAsStringAsync().Result);
            CollectionAssert.AreEquivalent(expected, actual);

            return actual;
        }
    }
}
