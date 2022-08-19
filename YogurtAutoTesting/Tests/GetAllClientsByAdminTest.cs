using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Tests.TestSources;

namespace YogurtAutoTesting.Tests
{
    public class GetAllClientsByAdminTest
    {
        private BaseClearCommand _deleteFromDb;
        private AuthorizationSteps _authorizationSteps;
        private ClientsSteps _clientsSteps;
        private CleanerSteps _cleanerSteps;
        private ServiceSteps _serviceSteps;
        private ClientMapper _clientMapper;
        private List<int> _ids;
        private string _adminToken;

        public GetAllClientsByAdminTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _clientsSteps = new ClientsSteps();
            _cleanerSteps = new CleanerSteps();
            _serviceSteps = new ServiceSteps();
            _deleteFromDb = new BaseClearCommand();
            _clientMapper = new ClientMapper();
            _ids = new List<int>();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _deleteFromDb.ClearBase();

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "Admin@gmail.com",
                Password = "qwerty12345",
            };
            _adminToken = _authorizationSteps.Authorize(authModel);
        }
        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }

        [TestCaseSource(typeof(RegisterThreeClients_WhenModelIsCorrect_TestCaseSource))]
        public void GetAllClients_WhenMClientsAreExist_ShouldGetAllClients(List<ClientRequestModel> clientsModel)
        {
            DateTime regDate = DateTime.Now.Date;
            foreach (ClientRequestModel clientRequest in clientsModel)
            {
                int id = _authorizationSteps.RegisterClient(clientRequest);
                _ids.Add(id);
            }
            List<ClientResponseModel> expectedModel = new List<ClientResponseModel>();
            for (int i = 0; i < _ids.Count; i++)
            {
                expectedModel.Add(
                    new ClientResponseModel
                {
                    Id = _ids[i],
                    LastName = clientsModel[i].LastName,
                    FirstName = clientsModel[i].FirstName,
                    BirthDate = clientsModel[i].BirthDate,
                    Email = clientsModel[i].Email,
                    Phone = clientsModel[i].Phone,
                    RegistrationDate = regDate
                });
            };
            _clientsSteps.GetAllClientsByClientIdByAdminTest(_adminToken, expectedModel);
        }

        [Test]
        public void GetAllClients_WhenListIsEmpty_ShouldGetEmptyList()
        {
            List<ClientResponseModel> expected = new List<ClientResponseModel>();
            _clientsSteps.GetAllClientsByClientIdByAdminTest(_adminToken, expected);
        }

        [TestCaseSource(typeof(ClientRegister_WhenModelIsCorrect_TestSource))]
        public void GetAllClients_WhenClientAuthorize_ShouldNotGetAllClients(ClientRequestModel clientModel, AuthRequestModel clientAuth)
        {
            _authorizationSteps.RegisterClient(clientModel);
            string clientToken = _authorizationSteps.Authorize(clientAuth);
            _clientsSteps.GetAllClientsWhenAdminDoNotAuthorizeTest(clientToken);
        }
        [TestCaseSource(typeof(AddCleaner_WhenModelIsCorrect_TestCaseSource))]
        public void GetAllClient_WhenCleanerAuthorize_ShouldNotGetAllClients(ServicesRequestModel serviceModel, CleanerRequestModel cleanerRequest, AuthRequestModel cleanerAuth)
        {
            int serviceId = _serviceSteps.CreateServiceTest(serviceModel, _adminToken);
            cleanerRequest.ServicesIds = new List<int> { serviceId };
            _cleanerSteps.CreateCleanerTest(cleanerRequest);
            string cleanerToken = _authorizationSteps.Authorize(cleanerAuth);
            _clientsSteps.GetAllClientsWhenAdminDoNotAuthorizeTest(cleanerToken);

        }
        [Test]
        public void GetAllClients_WhenUnauthorize_ShouldNotGetAllClients()
        {
            string token = "";
            _clientsSteps.GetAllClientsWhenUnauthorizedTest(token);
        }
    }
}
