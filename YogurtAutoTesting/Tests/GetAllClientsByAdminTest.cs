using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class GetAllClientsByAdminTest
    {
        private BaseClearCommand _deleteFromDb;
        private AuthorizationSteps _authorizationSteps;
        private ClientsSteps _clientsSteps;
        private List<int> _ids;
        private string _adminToken;

        public GetAllClientsByAdminTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _clientsSteps = new ClientsSteps();
            _deleteFromDb = new BaseClearCommand();
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

        [Test]
        public void GetAllClientsTest()
        {
            List<ClientRequestModel> clientsModel = new List<ClientRequestModel>()
            {
                new ClientRequestModel()
                {
                    FirstName = "Константин",
                    LastName = "Придуманный",
                    BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                    Password = "thebestKostya666",
                    ConfirmPassword = "thebestKostya666",
                    Email = "kostik08@gmail.com",
                    Phone = "89996662233"
                },
                new ClientRequestModel()
                {
                    FirstName = "Винни",
                    LastName = "Пух",
                    BirthDate = new DateTime(1921, 08, 21, 00, 00, 00),
                    Password = "KristoferRobin",
                    ConfirmPassword = "KristoferRobin",
                    Email = "WinnieThePooh@gmail.com",
                    Phone = "89998887766"
                },
            };
            DateTime regDate = DateTime.Now.Date;
            foreach (ClientRequestModel clientRequest in clientsModel)
            {
                int id = _authorizationSteps.RegisterClient(clientRequest);
                _ids.Add(id);
            }

            List<ClientResponseModel> expectedModel = new List<ClientResponseModel>();
            for (int i = 0; i < _ids.Count; i++)
            {
                expectedModel.Add(new ClientResponseModel
                {
                    Id = _ids[i],
                    LastName = clientsModel[i].LastName,
                    FirstName = clientsModel[i].FirstName,
                    BirthDate = clientsModel[i].BirthDate,
                    Email = clientsModel[i].Email,
                    Phone = clientsModel[i].Phone,
                    RegistrationDate = regDate.Date

                });
            };

            _clientsSteps.GetAllClientsByClientIdByAdminTest(_adminToken, expectedModel);

        }
    }
}
