using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Tests.TestSources;

namespace YogurtAutoTesting.Tests
{
    public class UpdateCleanerTest
    {
        private AuthorizationSteps _authorizationSteps;
        private CleanerSteps _cleanerSteps;
        private ServiceSteps _serviceSteps;
        private BaseClearCommand _deleteFromDb;
        int _cleanerId;
        int _serviceId;
        string _adminToken;
        string _cleanerToken;

        public UpdateCleanerTest()
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
        }
        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }
        [TestCaseSource(typeof(AddCleaner_WhenModelIsCorrect_TestCaseSource))]
        public void UpdateCleaner_WhenModelIsCorrect_ShoulUpdateCleaner(AuthRequestModel authModel,CleanerRequestModel cleanerModel, ServicesRequestModel serviceModel)
        {
            _adminToken = _authorizationSteps.Authorize(authModel);
            _serviceId = _serviceSteps.CreateServiceTest(serviceModel, _adminToken);
            _cleanerId = _cleanerSteps.CreateCleanerTest(cleanerModel);

            AuthRequestModel cleanerAuthorizeModel = new AuthRequestModel()
            {
                Email = cleanerModel.Email,
                Password = cleanerModel.Password
            };

            _cleanerToken = _authorizationSteps.Authorize(cleanerAuthorizeModel);

            UpdateCleanerRequestModel updateModel = new UpdateCleanerRequestModel()
            {
                FirstName = "Ирэн",
                LastName = "Иванова",
                BirthDate = cleanerModel.BirthDate,
                Phone = "87774441122",
                Id = _cleanerId,
                Districts = new List<int> { 5, 8, 4 },
                ServicesIds = new List<int> { _serviceId }
            };

            _cleanerSteps.UpdateCleanerByIdTest(_cleanerId, _cleanerToken, updateModel);

            CleanerResponseModel responseModel = new CleanerResponseModel()
            {
                Id = updateModel.Id,
                FirstName = updateModel.FirstName,
                LastName = updateModel.LastName,
                BirthDate = updateModel.BirthDate,
                Email = cleanerModel.Email,
                Phone = updateModel.Phone,
                DateOfStartWork = DateTime.Now.Date
            };
            _cleanerSteps.GetCleanerById(_cleanerId, _cleanerToken, responseModel);

        }
    }
}
