using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;

namespace YogurtAutoTesting.Tests
{
    public class AddCommentsTest
    {
        private AuthorizationSteps _authorizationSteps;
        private CommentsSteps _commentsSteps;
        private ClientsSteps _clientsSteps;
        private BaseClearCommand _deleteFromDb;
        private string _token;
        private int _clientId;

        public AddCommentsTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _commentsSteps = new CommentsSteps();
            _deleteFromDb = new BaseClearCommand();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _deleteFromDb.ClearBase();
            ClientRequestModel clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "thebestKostya666",
                ConfirmPassword = "thebestKostya666",
                Email = "kostik@gmail.com",
                Phone = "89996662233"
            };

            _clientId = _authorizationSteps.RegisterClient(clientRequest);

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = clientRequest.Email,
                Password = clientRequest.Password,
            };

            _token = _authorizationSteps.Authorize(authModel);
        }

        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }

        [Test]
        public void CreateNewCommentByClient_WhenModelIsCorrect_ShouldCreateComment()
        {
            CommentsRequestModel commentsRequest = new CommentsRequestModel()
            {
                Summary = "Хорошие клинеры, и уборка интересная",
                OrderId = 1,
                Rating = 5
            };
            int commentId = _commentsSteps.AddCommentTest(commentsRequest, _token);

            List<CommentsResponseModel> expectedCommentsResponse = new List<CommentsResponseModel>()
            {
                new CommentsResponseModel()
                {
                    Summary = commentsRequest.Summary,
                    OrderId = commentsRequest.OrderId,
                    Rating = commentsRequest.Rating
                }
            };
            _clientsSteps.GetCommentsByClientIdTest(_clientId, _token, expectedCommentsResponse);

        }
    }
}

