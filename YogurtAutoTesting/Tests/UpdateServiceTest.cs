using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class UpdateServiceTest
    {
        private AuthorizationSteps _authorizationSteps;
        private ServiceSteps _serviceSteps;
        private BaseClearCommand _deleteFromDb;
        private ServicesMapper _servicesMapper;
        int _serviceId;
        string _adminToken;

        public UpdateServiceTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _serviceSteps = new ServiceSteps();
            _deleteFromDb = new BaseClearCommand();
            _servicesMapper = new ServicesMapper();
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
        public void UpdateService_WhenModelIsCorrect_ShouldUpdateService()
        {
            ServicesRequestModel updateModel = new ServicesRequestModel()
            {
                Name = "Помыть холодильник",
                Price = 500.00,
                Unit = "Кухня",
                RoomType = 1,
                Duration = 60
            };
            _serviceSteps.UpdateServiceById(_serviceId, _adminToken, updateModel);

            ServicesResponseModel expected = _servicesMapper.MappServiceRequestModelToServiceResponseModel(updateModel, _serviceId);
            _serviceSteps.GetServiceByIdTest(_serviceId, _adminToken, expected);
        }


    }
}
