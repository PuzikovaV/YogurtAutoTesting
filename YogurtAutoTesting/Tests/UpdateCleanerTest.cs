using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
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
        private ServicesMapper _servicesMapper;
        int _cleanerId;
        int _serviceId;
        string _adminToken;

        public UpdateCleanerTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _cleanerSteps = new CleanerSteps();
            _serviceSteps = new ServiceSteps();
            _deleteFromDb = new BaseClearCommand();
            _servicesMapper = new ServicesMapper();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _deleteFromDb.ClearBase();
            AuthRequestModel adminAuthModel = new AuthRequestModel()
            {
                Email = "Admin@gmail.com",
                Password = "qwerty12345"
            };
            _adminToken = _authorizationSteps.Authorize(adminAuthModel);
        }
        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }
        [TestCaseSource(typeof(AddCleaner_WhenModelIsCorrect_TestCaseSource))]
        public void UpdateCleaner_WhenModelIsCorrect_ShoulUpdateCleaner(ServicesRequestModel serviceModel,CleanerRequestModel cleanerModel, AuthRequestModel authModel)
        {
            _serviceId = _serviceSteps.CreateServiceTest(serviceModel, _adminToken);
            cleanerModel.ServicesIds = new List<int> { _serviceId };
            _cleanerId = _cleanerSteps.CreateCleanerTest(cleanerModel);
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
            _cleanerSteps.UpdateCleanerByIdTest(_cleanerId, _adminToken, updateModel);
            List<ServicesResponseModel> expectedServices = new List<ServicesResponseModel>();
            expectedServices.Add(_servicesMapper.MappServiceRequestModelToServiceResponseModel(serviceModel, _serviceId));
            CleanerResponseModel responseModel = new CleanerResponseModel()
            {
                Id = updateModel.Id,
                FirstName = updateModel.FirstName,
                LastName = updateModel.LastName,
                BirthDate = updateModel.BirthDate,
                Email = cleanerModel.Email,
                Phone = updateModel.Phone,
                DateOfStartWork = DateTime.Now.Date,
                Rating = 0,
                Services = expectedServices
            };
            _cleanerSteps.GetCleanerById(_cleanerId, _adminToken, responseModel);
        }
    }
}
