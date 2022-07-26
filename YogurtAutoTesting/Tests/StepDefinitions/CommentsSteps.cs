﻿using System.Net;
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

        public int AddCommentByClientTest(CommentsRequestModel model, string token)
        {
            HttpContent content = _commentsClient.AddCommentByClient(model, token, HttpStatusCode.Created);
            int id = Convert.ToInt32(content.ReadAsStringAsync().Result);

            Assert.IsTrue(id > 0);

            return id;
        }

        public int AddCommentByCleanerTest(CommentsRequestModel model, string token)
        {
            HttpContent content = _commentsClient.AddCommentByCleaner(model, token, HttpStatusCode.Created);
            int id = Convert.ToInt32(content.ReadAsStringAsync().Result);

            Assert.IsTrue(id > 0);

            return id;
        }

        public void DeleteCommentByIdTest(int id, string token)
        {
            _commentsClient.DeleteCommentById(id, token, HttpStatusCode.NoContent);
        }

        public List<CommentsByClientResponseModel> GetAllCommentsByAdminTest(string token, List<CommentsByClientResponseModel> expected)
        {
            HttpContent httpContent = _commentsClient.GetAllComments(token, HttpStatusCode.OK);
            string content = httpContent.ReadAsStringAsync().Result;
            List<CommentsByClientResponseModel> actual = JsonSerializer.Deserialize<List<CommentsByClientResponseModel>>(content);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }


    }
}
