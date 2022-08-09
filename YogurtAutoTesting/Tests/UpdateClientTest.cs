using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;

namespace YogurtAutoTesting.Tests
{
    public class UpdateClientTest
    {
        private AuthorizationSteps _authorizationSteps;
        private ClientsSteps _clientsSteps;
        private BaseClearCommand _deleteFromDb;
        private int _clientId;
        private string _token;
        private ClientRequestModel _clientRequest;

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
            _clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "thebestKostya666",
                ConfirmPassword = "thebestKostya666",
                Email = "kostik08@gmail.com",
                Phone = "89996662233"
            };

            _clientId = _authorizationSteps.RegisterClient(_clientRequest);

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = _clientRequest.Email,
                Password = _clientRequest.Password,
            };

            _token = _authorizationSteps.Authorize(authModel);
        }

        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }

        [Test]
        public void UpdateClient_WhenModelIsCorrect_ShouldUpdateClient()
        {
            UpdateClientRequestModel clientUpdateRequest = new UpdateClientRequestModel()
            {
                FirstName = "Геннадий",
                LastName = "Крокодилов",
                Phone = "89995645456",
                BirthDate = new DateTime(1978, 05, 08, 00, 00, 00)
            };

            _clientsSteps.UpdateClientById(_clientId, _token, clientUpdateRequest);

            DateTime regTime = _clientsSteps.GetRegisterDate(_clientId, _token);

            ClientResponseModel expectedClient = new ClientResponseModel()
            {
                Id = _clientId,
                FirstName = clientUpdateRequest.FirstName,
                LastName = clientUpdateRequest.LastName,
                Email = _clientRequest.Email,
                Phone = clientUpdateRequest.Phone,
                BirthDate = clientUpdateRequest.BirthDate,
                RegistrationDate = regTime
            };

            _clientsSteps.GetClientByIdTest(_clientId, _token, expectedClient);

        }  
    }
}
