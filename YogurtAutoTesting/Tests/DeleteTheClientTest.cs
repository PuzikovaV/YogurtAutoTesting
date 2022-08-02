using System;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
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

            DateTime regDate = _clientsSteps.GetRegisterDate(clientId, token);

            ClientResponseModel expectedModel = new ClientResponseModel()
            {
                Id = clientId,
                FirstName = clientRequest.FirstName,
                LastName = clientRequest.LastName,
                BirthDate = clientRequest.BirthDate,
                Email = clientRequest.Email,
                Phone = clientRequest.Phone,
                RegistrationDate = regDate
            };

            _clientsSteps.GetClientByIdTest(clientId, token, expectedModel);
        }

    }
}
