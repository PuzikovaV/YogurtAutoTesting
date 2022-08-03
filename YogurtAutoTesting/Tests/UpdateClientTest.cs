using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.TestSources;

namespace YogurtAutoTesting.Tests
{
    public class UpdateClientTest
    {
        private AuthorizationSteps _authorizationSteps;
        private ClientsSteps _clientsSteps;
        private BaseClearCommand _deleteFromDb;

        public UpdateClientTest()
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
        public void UpdateClient_WhenModelIsCorrect_ShouldUpdateClient(ClientRequestModel clientRequest)
        {
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
