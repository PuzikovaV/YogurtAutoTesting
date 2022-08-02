using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class UpdateClientTest
    {
        AuthorizationSteps _authorizationSteps;
        ClientsSteps _clientsSteps;

        public UpdateClientTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _clientsSteps = new ClientsSteps();
        }
        [Test]
        public void UpdateClient_WhenModelIsCorrect_ShouldUpdateClient()
        {
            ClientRequestModel clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "thebestKostya666",
                ConfirmPassword = "thebestKostya666",
                Email = "kostik9978@gmail.com",
                Phone = "89996662233"
            };

            int clientId = _authorizationSteps.RegisterClient(clientRequest);

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = clientRequest.Email,
                Password = clientRequest.Password,
            };

            string token = _authorizationSteps.Authorize(authModel);

            UpdateClientRequestModel clientUpdateRequest = new UpdateClientRequestModel()
            {
                FirstName = "Геннадий",
                LastName = "Крокодилов",
                Phone = "89995645456",
                BirthDate = new DateTime(1978, 05, 08, 00, 00, 00)
            };

            _clientsSteps.UpdateClientById(clientId, token, clientUpdateRequest);

            DateTime regTime = _clientsSteps.GetRegisterDate(clientId, token);

            ClientResponseModel expectedClient = new ClientResponseModel()
            {
                Id = clientId,
                FirstName = clientUpdateRequest.FirstName,
                LastName = clientUpdateRequest.LastName,
                Email = clientRequest.Email,
                Phone = clientUpdateRequest.Phone,
                BirthDate = clientUpdateRequest.BirthDate,
                RegistrationDate = regTime
            };

            _clientsSteps.GetClientByIdTest(clientId, token, expectedClient);

        }  
    }
}
