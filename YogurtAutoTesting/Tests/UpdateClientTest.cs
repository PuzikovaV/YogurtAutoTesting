using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
using YogurtAutoTesting.Tests.TestSources;

namespace YogurtAutoTesting.Tests
{
    public class UpdateClientTest
    {
        private AuthorizationSteps _authorizationSteps;
        private ClientsSteps _clientsSteps;
        private BaseClearCommand _deleteFromDb;
        private ClientMapper _clientMapper;
        private int _clientId;
        private string _token;
        private ClientRequestModel _clientRequest;
        private CleanerSteps _cleanerSteps;

        public UpdateClientTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _clientsSteps = new ClientsSteps();
            _cleanerSteps = new CleanerSteps();
            _deleteFromDb = new BaseClearCommand();
            _clientMapper = new ClientMapper();
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
            DateTime regTime = DateTime.Now.Date;
            ClientResponseModel expectedClient = _clientMapper.MappUpdateClientRequestModelToClientResponseModel(clientUpdateRequest, _clientId, regTime, _clientRequest.Email);
            _clientsSteps.GetClientByIdTest(_clientId, _token, expectedClient);
        }
        [Test]
        public void UpdateClient_WhenAdminUpdate_ShouldUpdateClient()
        {
            AuthRequestModel adminModel = new AuthRequestModel()
            {
                Email = "Admin@gmail.com",
                Password = "qwerty12345"
            };
            string adminToken = _authorizationSteps.Authorize(adminModel);
            UpdateClientRequestModel clientUpdateRequest = new UpdateClientRequestModel()
            {
                FirstName = "Геннадий",
                LastName = "Крокодилов",
                Phone = "89995645456",
                BirthDate = new DateTime(1978, 05, 08, 00, 00, 00)
            };
            _clientsSteps.UpdateClientById(_clientId, adminToken, clientUpdateRequest);
            DateTime regTime = DateTime.Now.Date;
            ClientResponseModel expectedClient = _clientMapper.MappUpdateClientRequestModelToClientResponseModel(clientUpdateRequest, _clientId, regTime, _clientRequest.Email);
            _clientsSteps.GetClientByIdTest(_clientId, _token, expectedClient);
        }
        [TestCase (0)]
        [TestCase (-5)]
        public void UpdateClient_ByWrongId_ShouldDoNotUpdate(int id)
        {
            UpdateClientRequestModel updateModel = new UpdateClientRequestModel();
            _clientsSteps.UpdateClientByIdIsWrongTest(id, _token, updateModel);
        }
        [Test]
        public void UpdateClient_WhenUserUnauthorized_ShouldThrowException()
        {
            string token = "";
            UpdateClientRequestModel updateModel = new UpdateClientRequestModel();
            _clientsSteps.UpdateClientByIdWhenUserUnauthorizeTest(_clientId, token, updateModel);
        }
        [TestCaseSource(typeof(RegisterCleaner_WhenServiceIsEmpty_TestCaseSource))]
        public void UpdateClient_WhenCleanerTryToUpdate_ShouldException(CleanerRequestModel cleanerModel, AuthRequestModel cleanerAuth)
        {
            _cleanerSteps.CreateCleanerTest(cleanerModel);
            string cleanerToken = _authorizationSteps.Authorize(cleanerAuth);
            UpdateClientRequestModel updateModel = new UpdateClientRequestModel();
            _clientsSteps.UpdateClientByIdByCleanerTest(_clientId, cleanerToken, updateModel);
        }
        [Test]
        public void UpdateClient_ByAnotherId_ShouldException()
        {
            int id = _clientId++;
            UpdateClientRequestModel updateModel = new UpdateClientRequestModel();
            _clientsSteps.UpdateClientByIdIsWrongTest(id, _token, updateModel);
        }    
    }
}
