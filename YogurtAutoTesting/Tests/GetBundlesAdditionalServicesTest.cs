using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class GetBundlesAdditionalServicesTest
    {
        private BundlesSteps _bundleSteps;
        private ServiceSteps _serviceSteps;
        private AuthorizationSteps _authorizationSteps;
        private BaseClearCommand _deleteFromDb;
        ServicesRequestModel _serviceModel;
        int _serviceId;
        int _bundleId;
        string _adminToken;

        public GetBundlesAdditionalServicesTest()
        {
            _bundleSteps = new BundlesSteps();
            _serviceSteps = new ServiceSteps();
            _authorizationSteps = new AuthorizationSteps();
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

            _serviceModel = new ServicesRequestModel()
            {
                Name = "Помыть микроволновку",
                Price = 300.00,
                Unit = "Кухня",
                RoomType = 2,
                Duration = 15
            };
            _serviceId = _serviceSteps.CreateServiceTest(_serviceModel, _adminToken);

            BundlesRequestModel bundleModel = new BundlesRequestModel()
            {
                Name = "Ежедневная уборка",
                Type = 1,
                Price = 3000,
                Duration = 120,
                Measure = 2,
                ServicesIds = new List<int>() { _serviceId }
            };
            _bundleId = _bundleSteps.CreateBundleTest(bundleModel, _adminToken);
        }
        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }

        [Test]
        public void GetBundlesAdditionalServices_WhenIdIsCorrect_ShouldGetBundlesAdditionalServices()
        {
            List<ServicesResponseModel> expectedModel = new List<ServicesResponseModel>()
            {
                new ServicesResponseModel()
                {
                    Id = _serviceId,
                    Name = _serviceModel.Name,
                    Duration = _serviceModel.Duration,
                    Price = _serviceModel.Price,
                    RoomType = _serviceModel.RoomType,
                    Unit = _serviceModel.Unit
                }
            };
            _bundleSteps.GetBundlesAdditionalServicesByIdTest(_bundleId, _adminToken, expectedModel);
        }

    }
}
