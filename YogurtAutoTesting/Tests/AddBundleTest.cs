using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class AddBundleTest
    {
        private BundlesSteps _bundleSteps;
        private ServiceSteps _serviceSteps;
        private AuthorizationSteps _authorizationSteps;
        private BaseClearCommand _deleteFromDb;
        private ServicesRequestModel _serviceModel;
        int _serviceId;
        int _bundleId;
        string _adminToken;

        public AddBundleTest()
        {
            _bundleSteps = new BundlesSteps();
            _serviceSteps = new ServiceSteps();
            _authorizationSteps = new AuthorizationSteps();
            _deleteFromDb = new BaseClearCommand();
            _serviceModel = new ServicesRequestModel();
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
        }
        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }
        [Test]
        public void CreateBundle_WhenModelIsCorrect_ShouldCreateBundle()
        {
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

            BundlesResponseModel expectedModel = new BundlesResponseModel()
            {
                Name = bundleModel.Name,
                Type = bundleModel.Type,
                Price = bundleModel.Price,
                Duration = bundleModel.Duration,
                Measure = bundleModel.Measure,
                ServicesIds = new List<ServicesResponseModel>
                {
                    new ServicesResponseModel
                    {
                        Name = _serviceModel.Name,
                        Duration = _serviceModel.Duration,
                        Price = _serviceModel.Price,
                        Unit = _serviceModel.Unit,
                        RoomType = _serviceModel.RoomType,
                        Id = _serviceId
                    }
                },
                Id = _bundleId
            };
            _bundleSteps.GetBundleByIdTest(_bundleId, _adminToken, expectedModel);
            List<BundlesResponseModel> expectedBundlesList = new List<BundlesResponseModel>()
            {
                expectedModel
            };
            _bundleSteps.GetAllBundlesTest(_adminToken, expectedBundlesList);
        }

    }
}
