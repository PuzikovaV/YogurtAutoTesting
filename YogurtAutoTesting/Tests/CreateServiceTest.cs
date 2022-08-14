using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class CreateServiceTest
    {
        private AuthorizationSteps _authorizationSteps;
        private ServiceSteps _serviceSteps;
        private BaseClearCommand _baseClearCommand;
        string _adminToken;

        public CreateServiceTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _serviceSteps = new ServiceSteps();
            _baseClearCommand = new BaseClearCommand();
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
                Duration = 15
            };
            int serviseId = _serviceSteps.CreateServiceTest(model, _adminToken);

            ServicesResponseModel expected = new ServicesResponseModel()
            {
                Id = serviseId,
                Name = model.Name,
                Price = model.Price,
                Unit = model.Unit,
                Duration = model.Duration
            };

            _serviceSteps.GetServiceByIdTest(serviseId, _adminToken, expected);
            List<ServicesResponseModel> expectedList = new List<ServicesResponseModel>()
            {
                expected,
            };
            _serviceSteps.GetAllServicesTest(_adminToken, expectedList);
        }
    }
}
