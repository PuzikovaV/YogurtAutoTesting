using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
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
        private BundleMapper _bundleMapper;
        private ServicesMapper _servicesMapper;
        private List<ServicesResponseModel> _servicesList;
        private int _serviceId;
        private int _bundleId;
        private string _adminToken;

        public AddBundleTest()
        {
            _bundleSteps = new BundlesSteps();
            _serviceSteps = new ServiceSteps();
            _authorizationSteps = new AuthorizationSteps();
            _deleteFromDb = new BaseClearCommand();
            _serviceModel = new ServicesRequestModel();
            _bundleMapper = new BundleMapper();
            _servicesMapper = new ServicesMapper();
            _servicesList = new List<ServicesResponseModel>();
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
                RoomType = 1,
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
                RoomType = 1,
                ServicesIds = new List<int>(){_serviceId}
            };
            _bundleId = _bundleSteps.CreateBundleTest(bundleModel, _adminToken);
            _servicesList.Add(_servicesMapper.MappServiceRequestModelToServiceResponseModel(_serviceModel, _serviceId));
            BundlesResponseModel expectedModel = _bundleMapper.MappBundleRequestModelToBundleResponseModel(bundleModel, _servicesList, _bundleId);
            _bundleSteps.GetBundleByIdTest(_bundleId, _adminToken, expectedModel);
            List<BundlesResponseModel> expectedBundlesList = new List<BundlesResponseModel>()
            {
                expectedModel
            };
            _bundleSteps.GetAllBundlesTest(_adminToken, expectedBundlesList);
        }

    }
}
