using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.TestSources;
using YogurtAutoTesting.Support.Mappers;

namespace YogurtAutoTesting.Tests
{
    public class RegistrationOfAClient
    {
        private AuthorizationSteps _authorizationSteps;
        private ClientsSteps _clientsSteps;
        private BaseClearCommand _deleteFromDb;
        private ClientMapper _clientMapper;
        public RegistrationOfAClient()
        {
            _authorizationSteps = new AuthorizationSteps();
            _clientsSteps = new ClientsSteps();
            _deleteFromDb = new BaseClearCommand();
            _clientMapper = new ClientMapper();
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
        public void ClientCreate_WhenClientModelIsCorrect_ShouldCreateClient(ClientRequestModel clientRequest)
        {
            int id = _authorizationSteps.RegisterClient(clientRequest);

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = clientRequest.Email,
                Password = clientRequest.Password,
            };

            string token = _authorizationSteps.Authorize(authModel);

            DateTime regTime = _clientsSteps.GetRegisterDate(id, token);

            ClientResponseModel expectedClient = _clientMapper.MappClientRequestModelToClientResponseModel(clientRequest, id, regTime);

            _clientsSteps.GetClientByIdTest(id, token, expectedClient);
        }

        [TestCaseSource(typeof(ClientRegister_WhenPasswordLessThenEightSymbols_TestSource))]
        public void CreateClient_WhenPasswordLessThenEightSymbols_ShouldNotRegistrate(ClientRequestModel clientRequest)
        {
            _authorizationSteps.CantRegisterClientTest(clientRequest);
        }

        [TestCaseSource(typeof(ClientRegister_WhenPasswordIsSevenSymbols_TestSource))]
        public void CreateClient_WhenPasswordSeventSymbols_ShouldNotRegistrate(ClientRequestModel clientRequest)
        {
            _authorizationSteps.CantRegisterClientTest(clientRequest);
        }

        [TestCaseSource(typeof(ClientRegister_WhenModelIsCorrect_TestSource))]
        public void CreateClient_WhenEnterExistsEmail_ShouldNotRegistrate(ClientRequestModel clientRequest)
        {
            _authorizationSteps.RegisterClient(clientRequest);

            _authorizationSteps.CantRegisterClientTest(clientRequest);
        }

    }
}
