using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class DeleteTheClientTest
    {
        private AuthorizationSteps _authorizationSteps = new AuthorizationSteps();
        private ClientsSteps _clientsSteps = new ClientsSteps();

        [Test]
        public void ClientIsDeleted_WhenIdIsCorrect_ShouldDeleteTheClient()
        {
            ClientRequestModel clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "thebestKostya666",
                ConfirmPassword = "thebestKostya666",
                Email = "kostik08@gmail.com",
                Phone = "89996662233"
            };

            int clientId = _authorizationSteps.RegisterClient(clientRequest);

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "Admin@gmail.com",
                Password = "qwerty12345",
            };
            string token = _authorizationSteps.Authorize(authModel);

            _clientsSteps.DeleteClientByAdminTest(clientId, token);
        }

    }
}
