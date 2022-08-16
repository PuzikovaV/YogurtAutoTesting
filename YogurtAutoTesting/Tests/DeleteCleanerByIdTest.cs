using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class DeleteCleanerByIdTest
    {
        private AuthorizationSteps _authorizationSteps;
        private CleanerSteps _cleanerSteps;
        private ServiceSteps _serviceSteps;
        private BaseClearCommand _deleteFromDb;
        string _adminToken;
        string _cleanerToken;
        int _serviceId;
        int _cleanerId;

        public DeleteCleanerByIdTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _cleanerSteps = new CleanerSteps();
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

            ServicesRequestModel requestModel = new ServicesRequestModel()
            {
                Name = "Помыть микроволновку",
                Price = 300.00,
                Unit = "Кухня",
                RoomType = 2,
                Duration = 15
            };
            _serviceId = _serviceSteps.CreateServiceTest(requestModel, _adminToken);

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
                ServicesIds = new List<int>(_serviceId)
            };
            _cleanerId = _cleanerSteps.CreateCleanerTest(cleanerModel);

            AuthRequestModel authCleanerModel = new AuthRequestModel()
            {
                Email = cleanerModel.Email,
                Password = cleanerModel.Password,
            };
            _cleanerToken = _authorizationSteps.Authorize(authCleanerModel);

        }
        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }

        public void DeleteCleanerById_WhenModelIsCorrect_ShoulDeleteCleaner()
        {
            _cleanerSteps.DeleteCleanerByIdTest(_cleanerId, _cleanerToken);

            List<CleanerResponseModel> expected = new List<CleanerResponseModel>();

            _cleanerSteps.GetAllCleanersByAdminTest(_adminToken, expected);
        }
    }
}
