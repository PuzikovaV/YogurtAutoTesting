using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class RegisterCleanerTest
    {
        private AuthorizationSteps _authorizationSteps;
        private CleanerSteps _cleanerSteps;
        private ServiceSteps _serviceSteps;
        private BaseClearCommand _deleteFromDb;
        int _serviceId;
        string _adminToken;
        private ServicesMapper _servicesMapper;
        private ServicesRequestModel _serviceRequest;

        public RegisterCleanerTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _cleanerSteps = new CleanerSteps();
            _serviceSteps = new ServiceSteps();
            _deleteFromDb = new BaseClearCommand();
            _servicesMapper = new ServicesMapper();
            _serviceRequest = new ServicesRequestModel();
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

            _serviceRequest = new ServicesRequestModel()
            {
                Name = "Помыть микроволновку",
                Price = 300.00,
                Unit = "Кухня",
                RoomType = 2,
                Duration = 15
            };
            _serviceId = _serviceSteps.CreateServiceTest(_serviceRequest, _adminToken);

        }
        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }
        [Test]
        public void RegisterCleaner_WhenModelIsCorrect_ShouldCreateCleaner()
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
                ServicesIds = new List<int>() {_serviceId}
            };
            DateTime regDate = DateTime.Now.Date;
            int cleanerId = _cleanerSteps.CreateCleanerTest(cleanerModel);
            AuthRequestModel cleanerAuthModel = new AuthRequestModel()
            {
                Email = cleanerModel.Email,
                Password = cleanerModel.Password,
            };
            string token = _authorizationSteps.Authorize(cleanerAuthModel);
            ServicesResponseModel servicesResponse = _servicesMapper.MappServiceRequestModelToServiceResponseModel(_serviceRequest, _serviceId);
            CleanerResponseModel expectedCleaner = new CleanerResponseModel()
            {
                Id = cleanerId,
                FirstName = cleanerModel.FirstName,
                LastName = cleanerModel.LastName,
                BirthDate = cleanerModel.BirthDate,
                Email = cleanerModel.Email,
                Phone = cleanerModel.Phone,
                DateOfStartWork = regDate,
                Services = new List<ServicesResponseModel>() { servicesResponse }
            };
            _cleanerSteps.GetCleanerById(cleanerId, token, expectedCleaner);
        }
    }
}
