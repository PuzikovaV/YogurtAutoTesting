using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.TestSources;

namespace YogurtAutoTesting.Tests
{
    public class DeleteTheClientTest
    {
        private AuthorizationSteps _authorizationSteps;
        private ClientsSteps _clientsSteps;
        private BaseClearCommand _deleteFromDb;

        public DeleteTheClientTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _clientsSteps = new ClientsSteps();
            _deleteFromDb = new BaseClearCommand();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _deleteFromDb.ClearBase();
        }

        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }

        [TestCaseSource(typeof(ClientRegister_WhenModelIsCorrect_TestSource))]
        public void ClientIsDeleted_WhenIdIsCorrect_ShouldDeleteTheClient(ClientRequestModel clientRequest, AuthRequestModel authModel)
        {
            int clientId = _authorizationSteps.RegisterClient(clientRequest);

            authModel = new AuthRequestModel()
            {
                Email = "Admin@gmail.com",
                Password = "qwerty12345",
            };
            string token = _authorizationSteps.Authorize(authModel);

            _clientsSteps.DeleteClientByAdminTest(clientId, token);

            AuthRequestModel authClientModel = new AuthRequestModel()
            {
                Email = clientRequest.Email,
                Password = clientRequest.Password
            };
            _authorizationSteps.DoNotAuthorizeTest(authClientModel);
        }

    }
}
