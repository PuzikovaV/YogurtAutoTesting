using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.TestSources;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Tests
{
    public class DeleteTheClientTest
    {
        private AuthorizationSteps _authorizationSteps;
        private ClientsSteps _clientsSteps;
        private CleanerSteps _cleanerSteps;
        private BaseClearCommand _deleteFromDb;
        List<int> _clientsIds;
        private string _adminToken;

        public DeleteTheClientTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _clientsSteps = new ClientsSteps();
            _cleanerSteps = new CleanerSteps();
            _deleteFromDb = new BaseClearCommand();
            _clientsIds = new List<int>();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _deleteFromDb.ClearBase();
            AuthRequestModel authClientModel = new AuthRequestModel()
            {
                Email = "Admin@gmail.com",
                Password = "qwerty12345",
            };
            _adminToken = _authorizationSteps.Authorize(authClientModel);
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
        [TestCaseSource(typeof(RegisterThreeClients_WhenModelIsCorrect_TestCaseSource))]
        public void ClientIsDeleted_WhenIdIsCorrect_ShoulDeleteOneClientOfThree(List<ClientRequestModel> clientsModel)
        {
            foreach(ClientRequestModel clientModel in clientsModel)
            {
                _clientsIds.Add(_authorizationSteps.RegisterClient(clientModel));
            };
            int clientIdToDelete = 0;
            if(_clientsIds.Count > 1)
            {
                for (int i = 0; i < _clientsIds.Count - 1; i++)
                {
                    clientIdToDelete = _clientsIds[i];
                }
            }
            else if (_clientsIds.Count > 0)
            {
                for (int i = 0; i < _clientsIds.Count; i++)
                {
                    clientIdToDelete = _clientsIds[i];
                }
            }
            _clientsSteps.DeleteClientByAdminTest(clientIdToDelete, _adminToken);
            ClientResponseModel deletedClient = new ClientResponseModel();
            for (int i = 0; i < _clientsIds.Count; i++)
            {
                if(clientIdToDelete == _clientsIds[i])
                {
                    deletedClient = new ClientResponseModel()
                    {
                        Id = clientIdToDelete,
                        FirstName = clientsModel[i].FirstName,
                        LastName = clientsModel[i].LastName,
                        BirthDate = clientsModel[i].BirthDate,
                        Email = clientsModel[i].Email,
                        Phone = clientsModel[i].Phone,
                        RegistrationDate = DateTime.Now.Date
                    };
                };
            };
            _clientsSteps.GetAllClientsDoesNotContainOneByAdminTest(_adminToken, deletedClient);  
        }
        [TestCaseSource(typeof(ClientRegister_WhenModelIsCorrect_TestSource))]
        public void DeleteClient_WhenClientDeleteHimself_ShouldNoDeleteClient(ClientRequestModel clientModel, AuthRequestModel clientAuth)
        {
            int clientId = _authorizationSteps.RegisterClient(clientModel);
            string token = _authorizationSteps.Authorize(clientAuth);
            _clientsSteps.DoNotDeleteClientTest(clientId, token);
        }
        [TestCase (0)]
        [TestCase (-5)]
        public void DeleteClientById_WhenIdDoesNotCorrect_ShouldNotDelete(int id)
        {
            _clientsSteps.DeleteClientWithWrongIdTest(id, _adminToken);   
        }
        [TestCase (15)]
        public void DeleteClient_WhenUserUnauthorize_ShouldNotDeleteClient(int id)
        {
            string token = "";
            _clientsSteps.DeleteClientWhenUnauthorizeTest(id, token);
        }
        [TestCase (20)]
        public void DeleteClientById_WhenCleanerTryToDelete_ShouldNotDelete(int id)
        {
            CleanerRequestModel cleanerModel = new CleanerRequestModel()
            {
                FirstName = "Зина",
                LastName = "Корзина",
                BirthDate = new DateTime(1990, 08, 09),
                Email = "korzinka@zinka.ru",
                Password = "ZiZi1234",
                ConfirmPassword = "ZiZi1234",
                Passport = "1254789654",
                Phone = "89998887744",
                Schedule = 1,
                Districts = new List<int>() { 5, 6, 8 },
                ServicesIds = new List<int>()
            };
            _cleanerSteps.CreateCleanerTest(cleanerModel);
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = cleanerModel.Email,
                Password = cleanerModel.Password,
            };
            string cleanerToken = _authorizationSteps.Authorize(authModel);
            _clientsSteps.DoNotDeleteClientTest(id, cleanerToken);
        }
        

    }
}
