using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class CreateServiceTest
    {
        private AuthorizationSteps _authorizationSteps;
        private ServiceSteps _serviceSteps;
        private BaseClearCommand _baseClearCommand;
        private ServicesMapper _servicesMapper;
        string _adminToken;

        public CreateServiceTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _serviceSteps = new ServiceSteps();
            _baseClearCommand = new BaseClearCommand();
            _servicesMapper = new ServicesMapper();
        }
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _baseClearCommand.ClearBase();

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
            _baseClearCommand.ClearBase();
        }

        [Test]
        public void AddService_WhenModelIsCorrect_ShouldCreateService()
        {
            ServicesRequestModel model = new ServicesRequestModel()
            {
                Name = "Помыть микроволновку",
                Price = 300.00,
                Unit = "Кухня",
                RoomType = 2,
                Duration = 15
            };
            int serviseId = _serviceSteps.CreateServiceTest(model, _adminToken);

            ServicesResponseModel expected = _servicesMapper.MappServiceRequestModelToServiceResponseModel(model, serviseId);
            _serviceSteps.GetServiceByIdTest(serviseId, _adminToken, expected);
            List<ServicesResponseModel> expectedList = new List<ServicesResponseModel>()
            {
                expected,
            };
            _serviceSteps.GetAllServicesTest(_adminToken, expectedList);
        }
    }
}
