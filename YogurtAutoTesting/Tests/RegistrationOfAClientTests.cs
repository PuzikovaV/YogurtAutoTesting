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
        public void ClientCreate_WhenClientModelIsCorrect_ShouldCreateClient(ClientRequestModel clientRequest, AuthRequestModel authModel)
        {
            int id = _authorizationSteps.RegisterClient(clientRequest);

            string token = _authorizationSteps.Authorize(authModel);

            DateTime regTime = _clientsSteps.GetRegisterDate(id, token);

            ClientResponseModel expectedClient = _clientMapper.MappClientRequestModelToClientResponseModel(clientRequest, id, regTime);

            _clientsSteps.GetClientByIdTest(id, token, expectedClient);
        }

        [Test]
        public void CreateClient_WhenPasswordLessThenEightSymbols_ShouldNotRegistrate()
        {
            ClientRequestModel clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "the8",
                ConfirmPassword = "the8",
                Email = "kostik@gmail.com",
                Phone = "89996662233"
            };
            _authorizationSteps.CantRegisterClientTest(clientRequest);
        }

        [Test]
        public void CreateClient_WhenEnterExistsEmail_ShouldNotRegistrate()
        {
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

            _authorizationSteps.RegisterClient(clientRequest);

            _authorizationSteps.CantRegisterClientTest(clientRequest);
        }

    }
}
