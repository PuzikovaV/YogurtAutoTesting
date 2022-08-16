using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class DeleteServiceByIdTest
    {
        private AuthorizationSteps _authorizationSteps;
        private ServiceSteps _serviceSteps;
        private BaseClearCommand _deleteFromDb;
        string _adminToken;
        int _serviceId;

        public DeleteServiceByIdTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _serviceSteps = new ServiceSteps();
            _deleteFromDb = new BaseClearCommand();
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

            ServicesRequestModel serviceModel = new ServicesRequestModel()
            {
                Name = "Помыть микроволновку",
                Price = 300.00,
                Unit = "Кухня",
                RoomType = 2,
                Duration = 15
            };
            _serviceId = _serviceSteps.CreateServiceTest(serviceModel, _adminToken);
        }

        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }
        [Test]
        public void DeleteServiceById_WhenModelIsCorrect_ShoulDeleteService()
        {
            _serviceSteps.DeleteServiceById(_serviceId, _adminToken);
            List<ServicesResponseModel> expectedModel = new List<ServicesResponseModel>();
            _serviceSteps.GetAllServicesTest(_adminToken, expectedModel);
        }
    }
}
